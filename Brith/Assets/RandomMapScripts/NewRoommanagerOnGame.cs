using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace V
{
    public class NewRoommanagerOnGame : MonoBehaviour
    {
        public static NewRoommanagerOnGame Instance { get; set; }
        List<Room> RoomList;
        List<GameObject> Rooms = new();
        List<Vector3> RoomPosition;
        //Transform OriginPoint;
        Vector3 OriginPosition;
        public GameObject RoomPrefab;
        public GameObject DoorPrefab;
        float BaseRadius;//Basic radius��standard radius��

        #region Custom Varible
        public int MaxFindTime;
        public float XOffsetMAX;
        public float YOffsetMAX;
        [Header("����������")]
        public int RoomNumber;
        [Header("���������")]
        public float RoomIntervalMAX;//max interval between two rooms
        [Header("������С���")]
        public float RoomIntervalMIN;
        [Header("�󷿼�����")]
        public int BigRoomCount;
        [Header("�󷿼�뾶��Χ")]
        public float[] BigRoomRadius = new float[2];
        [Header("�з�������")]
        public int MediumRoomCount;
        [Header("�з���뾶��Χ")]
        public float[] MediumRoomRadius = new float[2];
        [Header("С��������")]
        public int SmallRoomCount;
        [Header("С����뾶��Χ")]
        public float[] SmallRoomRadius = new float[2];
        #endregion


        #region IsVisableInCamera
        public Camera Camera;
        #endregion
        #region Interface
        public List<GameObject> GetRooms()
        {
            return Rooms;
        }
        public List<Room> GetRoomList()
        {
            return RoomList;
        }
        #endregion
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one NewRoomManager");
            }

            Instance = this;
            BaseRadius = 0.5f;
            OriginPosition = gameObject.transform.position;
            CreateNewRoom();
        }
        private void Start()
        {

        }
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.B))
        //    {
        //        ClearRoom();

        //        IcanSeeInCameral();//test mst
        //    }

        //}
        #region IsVisableInCamera(Vector3 Position)
        /// <summary>
        /// to judge point if is in cameral
        /// </summary>
        public bool IsVisableInCamera(Vector3 Position)
        {
            //Debug.Log("IsVisableInCamera On");
            Vector3 viewPos = Camera.WorldToViewportPoint(Position);
            if (viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1)
                return false;
            return true;
        }
        public bool IsVisableInCamera(Vector3 Position, float Radius)
        {
            List<float[]> Offset = new List<float[]>();
            Offset.Add(new[] { Radius, 0 });
            Offset.Add(new[] { 0, Radius });
            Offset.Add(new[] { -Radius, 0 });
            Offset.Add(new[] { 0, -Radius });

            Vector3 viewPos = Camera.WorldToViewportPoint(Position);
            if (viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1)
                return false;
            for (int i = 0; i < 4; i++)
            {
                Vector3 tempPos = Camera.WorldToViewportPoint(new Vector3(Position.x + Offset[i][0], Position.y + Offset[i][1], 0));
                if (tempPos.x < 0 || tempPos.y < 0 || tempPos.x > 1 || tempPos.y > 1)
                    return false;
            }
            return true;
        }
        #endregion
        #region IsCoincide(Vector3 position)
        /// <summary>
        /// is this room coincide with other rooms
        /// </summary>
        public bool IsCoincide(Vector3 position, float radius, List<Room> RoomList)
        {
            // Debug.Log("IsCoincide");
            foreach (Room v in RoomList)
            {
                float tempDis = Vector3.Distance(position, v.Position);
                if (tempDis <= radius + v.Radius | tempDis < RoomIntervalMIN | tempDis > RoomIntervalMAX)
                    return false;
            }
            return true;
        }
        #endregion
        #region CreateNewRoom
        void CreateNewRoom()
        {
            RoomList = new List<Room>();

            CreateDifferentRoom(BigRoomCount, BigRoomRadius);
            CreateDifferentRoom(MediumRoomCount, MediumRoomRadius);
            CreateDifferentRoom(SmallRoomCount, SmallRoomRadius);

            CreateMST(RoomList.Count);
            CreateDoor();
            FindEndRoom();
            AddRoomData();
            RoomCreateEntity();
        }
        void CreateDifferentRoom(int number, float[] range)
        {
            for (int i = 0; i < number; i++)
            {
                #region to prevent stucking
                int KeyOut = 0;//to prevent stucking
                bool flag = false;
                #endregion
                float radius = UnityEngine.Random.Range(range[0], range[1]);
                Vector3 tempPos;
                while (true)
                {
                    tempPos = new Vector3(OriginPosition.x + UnityEngine.Random.Range(-XOffsetMAX, XOffsetMAX), OriginPosition.y + UnityEngine.Random.Range(-YOffsetMAX, YOffsetMAX), 0);
                    if (IsVisableInCamera(tempPos, radius) && IsCoincide(tempPos, radius, RoomList))
                    {
                        //Debug.Log("Find");
                        flag = true;
                        break;
                    }
                    #region to prevent stucking
                    KeyOut++;
                    if (KeyOut > MaxFindTime)
                        break;
                    #endregion
                }
                //save Room
                if (flag)
                {
                    Room tempRoom = new(tempPos, radius, i);
                    RoomList.Add(tempRoom);
                    GameObject go = Instantiate(RoomPrefab, tempPos, Quaternion.identity);
                    go.transform.localScale *= radius / 0.5f;
                    Rooms.Add(go);
                }
                Debug.Log(KeyOut);//find times
            }
        }
        float DIstanceBTRoom(Room roomA, Room roomB)
        {
            return (Vector3.Distance(roomA.Position, roomB.Position));
        }
        #endregion
        #region clear Room
        void ClearRoom()
        {
            for (int i = 0; i < Rooms.Count; i++)
            {
                Destroy(Rooms[i]);
            }
            Rooms.Clear();
        }
        #endregion

        #region MST
        void CreateMST(int number)
        {
            Graph g = new Graph(number);
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    if (i != j)
                    {
                        g.AddEdge(i, j, DIstanceBTRoom(RoomList[i], RoomList[j]));
                    }
                }
            }
            List<Edge> mst = g.KruskalMST();
            foreach (Edge e in mst)
            {
                RoomList[e.Source].Connect(e.Destination);
                RoomList[e.Destination].Connect(e.Source);
            }
        }
        #endregion
        #region CreateDoor
        void CreateDoor()
        {
            for (int i = 0; i < RoomList.Count; i++)
            {
                Room tempRoom = RoomList[i];
                List<int> tempConnectedRoom = tempRoom.GetConnectedRoom;
                for (int j = 0; j < tempConnectedRoom.Count; j++)
                {
                    int ConnctedRoom = tempConnectedRoom[j];
                    //Debug.Log(ConnctedRoom);
                    Vector3 tempDirection = FindDoor(tempRoom.Position, RoomList[ConnctedRoom].Position);
                    Vector3 Rotation = tempDirection.normalized;
                    //Debug.Log(Rotation);
                    Vector3 DoorOffset = (tempRoom.Radius - 0.1f) * Rotation;//move the door near the center of room
                    Vector3 DoorEndOffset = tempDirection - (RoomList[ConnctedRoom].Radius - 0.1f) * Rotation;////move the door near the center of room
                    GameObject tempDoor = Instantiate(DoorPrefab, tempRoom.Position + DoorOffset, Quaternion.identity);
                    Door door = tempDoor.GetComponent<Door>();
                    door.ConnectedRoom = ConnctedRoom;
                    //Debug.Log(tempDoor.GetComponent<Door>().ConnectedRoom);
                    door.EndPosition = tempRoom.Position + DoorEndOffset;
                    tempDoor.transform.SetParent(Rooms[i].transform, true);
                    Debug.Log(tempDoor.GetComponent<Door>().EndPosition);
                }
            }
        }
        Vector3 FindDoor(Vector3 A, Vector3 B)
        {
            return (B - A);
        }
        #endregion
        #region FindEndRoom which to put target
        void FindEndRoom()
        {
            bool[] VisitedRoom = new bool[RoomList.Count];
            Queue<Room> queue = new Queue<Room>();
            queue.Enqueue(RoomList[0]);
            VisitedRoom[0] = true;
            while (queue.Count > 0)
            {
                Room tempRoom = queue.Dequeue();
                foreach (int index in tempRoom.ConnectedRoom)
                {
                    if (!VisitedRoom[index])
                    {
                        RoomList[index].RoomNumberFromOrigin = tempRoom.RoomNumberFromOrigin + 1;
                        VisitedRoom[index] = true;
                        queue.Enqueue(RoomList[index]);
                    }
                }
            }
            int tempN = 0;
            int temPIndex = 0;
            for (int i = 1; i < RoomList.Count; i++)
            {
                if (RoomList[i].RoomNumberFromOrigin > tempN)
                {
                    temPIndex = i;
                    tempN = RoomList[i].RoomNumberFromOrigin;
                }

            }
            RoomList[temPIndex].EndRoom = true;
        }
        #endregion

        void AddRoomData()
        {
            for (int i = 0; i < Rooms.Count; i++)
            {
                Room room = RoomList[i];
                GameObject go = Rooms[i];
                RoomInfo roomInfo = go.GetComponent<RoomInfo>();
                roomInfo.EndRoom = room.EndRoom;
                roomInfo.Radius = room.Radius;
                roomInfo.RoomNumberFromOrigin = room.RoomNumberFromOrigin;
                roomInfo.Number = room.Number;
                roomInfo.ConnectedRoom = room.ConnectedRoom;
            }
        }
        void RoomCreateEntity()
        {
            foreach (GameObject go in Rooms)
            {
                go.GetComponentInChildren<SpawnEntity>().CreateEntity();
            }
        }
        #region text mst
        void IcanSeeInCameral()
        {
            foreach (Room room in RoomList)
            {
                room.IcanSee();
            }
            for (int i = 0; i < Rooms.Count; i++)
            {
                GameObject go = Rooms[i];
                go.GetComponentInChildren<TMP_Text>().text = RoomList[i].IcanSeeWay;
            }
        }
        #endregion
        public int CalculateBTRoom(int a, int b)
        {
            if(a==b)
                return 0;
            bool[] VisitedRoom = new bool[RoomList.Count];
            int[] Distance = new int[RoomList.Count];
            Queue<Room> queue = new Queue<Room>();
            queue.Enqueue(RoomList[a]);
            VisitedRoom[a] = true;
            Distance[a] = 0;
            while (queue.Count > 0)
            {
                Room tempRoom = queue.Dequeue();
                foreach (int index in tempRoom.ConnectedRoom)
                {
                    if (!VisitedRoom[index])
                    {
                        VisitedRoom[index] = true;
                        Distance[index] = Distance[tempRoom.Number]+1;
                        queue.Enqueue(RoomList[index]);
                    }
                    if (index == b)
                        return Distance[b];
                }
            }
            return Distance[b];
        }
    }

}
