namespace Script
{
    public struct Object
    {
        private float[] timeStamp;
        private int type, pos;

        public Object(float[] timeStamp, int pos, int type)
        {
            this.timeStamp = timeStamp;
            this.type = type;
            this.pos = pos;
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
    }
}