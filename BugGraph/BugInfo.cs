using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugGraph
{
    public class BugInfo
    {
        public int date;
        public int numberOpen;
        public int numberClosed;

        public BugInfo(int date, int numberOpen, int numberClosed)
        {
            this.date = date;
            this.numberOpen = numberOpen;
            this.numberClosed = numberClosed;
        }
    }
}
