using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2Academy.Data
{
    public class StringDictionaryInfo
    {
        private String name;
        private String content;


        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            if (name != null && !name.ToLower().Equals("null"))
            {
                this.name = name;
            }
        }

        public String getContent()
        {
            return content;
        }

        public void setContent(String content)
        {
            if (content != null && !content.ToLower().Equals("null"))
            {
                this.content = content;
            }
        }


    }
}

