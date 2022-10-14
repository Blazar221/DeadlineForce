using System;
namespace Script
{
    [Serializable] 
    public struct Object
    {
        public float[] timeStamp;
        public int type, pos, color;
        public bool isMain;
        

        public Object(float[] timeStamp, int pos, int type, bool isMain = false, int color = 0)
        {
            this.timeStamp = timeStamp;
            this.type = type;
            this.pos = pos;
            this.isMain = isMain;
            this.color = color;
        }

        public float[] TimeStamp
        {
            get { return timeStamp; }
        }

        public int Type
        {
            get { return type; }
        }

        public int Pos
        {
            get { return pos; }
        }

        public bool IsMain
        {
            get { return isMain; }
        }

        public int Color
        {
            get { return color; }
        }
    }
}