using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class Person {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class ValuesController : ApiController
    {
        //根据http动作匹配以动作名称开头的action,如果多个action动作名字相同,会根据参数进行耿精确的匹配

        //简单数据类型通过url传递，复杂数据类型通过请求体传递
        // GET api/values
        //返回全部数据
        public IEnumerable<Person> Getvalues()
        {
            return new List<Person> {
                new Person(){
                   ID=1,
                   Name="LQZ",
                   Age=20
                },
                 new Person(){
                   ID=2,
                   Name="YZ",
                   Age=19
                }
            };
        }

        // GET api/values/5
        //返回单条数据
        public Person Get(int id)//简单数据类型（非主体值 通过url传递）
        {
            return new Person()
            {
                ID = 2,
                Name = "YZ",
                Age = 19
            };
        }

        // POST api/values
        //添加
        public void Post(Person p)//复杂数据类型（主体值 只能有一个 通过请求体传递）
        {
        }

        // PUT api/values/5
        //修改
        public void Put( Person p)
        {
        }

        // DELETE api/values/5
        //删除
        public void Delete(int id)
        {
        }
    }
}
