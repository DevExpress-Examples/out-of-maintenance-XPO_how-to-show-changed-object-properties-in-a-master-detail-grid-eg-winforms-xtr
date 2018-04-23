using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace WindowsFormsApplication25 {

    public class DomainObject : BaseObject {

        public DomainObject(Session s) : base(s) { }
        
        [Key(true)]
        [Persistent("Oid")]
        private Guid _Oid;
        [PersistentAlias("_Oid")]
        public Guid Oid { get { return _Oid; } }

        private int _IntProperty;
        public int IntProperty { get { return _IntProperty; } set { SetPropertyValue("IntProperty", ref _IntProperty, value); } }
        private DateTime _DateTimeProperty;
        public DateTime DateTimeProperty { get { return _DateTimeProperty; } set { SetPropertyValue("DateTimeProperty", ref _DateTimeProperty, value); } }
        private decimal _DecimalProperty;
        public decimal DecimalProperty { get { return _DecimalProperty; } set { SetPropertyValue("DecimalProperty", ref _DecimalProperty, value); } }
        private string _StringProperty;
        public string StringProperty { get { return _StringProperty; } set { SetPropertyValue("StringProperty", ref _StringProperty, value); } }
    }
}
