using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace hypster.Controllers.helpers
{
    public class PlaylistHelper
    {


        public static string DisplayViews(int? viewsNum)
        {
            StringBuilder ret_val = new StringBuilder();


            if (viewsNum > 1000)
            {
                int vn = (int)viewsNum / 1000;

                ret_val.Append(vn).Append("K");

                if (vn > 1000)
                {
                    ret_val.Clear();
                    ret_val.Append(vn / 1000).Append("M");
                }
            }
            else
            {
                ret_val.Append(viewsNum);
            }


            return ret_val.ToString();
        }







    }
}