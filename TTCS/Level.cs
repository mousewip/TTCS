namespace TTCS
{
    public class Level
    {
        private static Level _instance;

        public static Level Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Level();
                }
                return _instance;
            }
        }

        private Level()
        {
            TongGiamDoc = new Pair("Tổng Giám Đốc", 1);
            GiamDoc = new Pair("Giám Đốc", 2);
            PhoGiamDoc = new Pair("Phó Giám Đốc", 3);
            TruongPhong = new Pair("Trưởng Phòng", 4);
            PhoPhong = new Pair("Phó Phòng", 5);
            ThuKy = new Pair("Thư Ký", 6);
            NhanVien = new Pair("Nhân Viên", 7);
            ListLevel = new Pair[]
            {
                TongGiamDoc,
                GiamDoc,
                PhoGiamDoc,
                TruongPhong,
                PhoPhong,
                ThuKy,
                NhanVien
            };
        }

        public Pair[] ListLevel;


        public Pair TongGiamDoc;
        public Pair GiamDoc;
        public Pair PhoGiamDoc;
        public Pair TruongPhong;
        public Pair PhoPhong;
        public Pair ThuKy;
        public Pair NhanVien;
    }
}
