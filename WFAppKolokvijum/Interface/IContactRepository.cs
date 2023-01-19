using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFAppKolokvijum.Model;

namespace WFAppKolokvijum.Interface
{
    public interface IContactRepository
    {
        public List<Contact> GetContacts();
        public List<Contact> GetContact(string name);
        public bool AddContact(Contact contact);
        public bool DeleteContact(int id);
        public bool UpdateContact(Contact contact);
    }
}
