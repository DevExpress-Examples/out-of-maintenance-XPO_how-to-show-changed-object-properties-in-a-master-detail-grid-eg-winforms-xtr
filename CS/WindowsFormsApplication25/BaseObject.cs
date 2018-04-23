using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace WindowsFormsApplication25 {

    public class PropertyData {
        private BaseObject owner;
        private XPMemberInfo member;
        public PropertyData(BaseObject owner, XPMemberInfo member) {
            this.owner = owner;
            this.member = member;
        }
        public string Name { get { return member.Name; } }
        public Type Type { get { return member.MemberType; } }
        public object OldValue { get { return owner.GetOldValue(member); } }
        public object NewValue { get { return member.GetValue(owner); } }
    }
    [NonPersistent]
    public class BaseObject : XPBaseObject {
        public BaseObject(Session s)
            : base(s) {
            oldValues = new Dictionary<XPMemberInfo, object>();
            properties = new List<PropertyData>();
            foreach (XPMemberInfo member in this.ClassInfo.PersistentProperties) {
                properties.Add(new PropertyData(this, member));
            }
        }
        private Dictionary<XPMemberInfo, object> oldValues;
        internal object GetOldValue(XPMemberInfo member) {
            object v;
            if (oldValues.TryGetValue(member, out v)) return v;
            return null;
        }
        private void SaveOldValues() {
            oldValues.Clear();
            foreach (XPMemberInfo member in this.ClassInfo.PersistentProperties) {
                oldValues.Add(member, member.GetValue(this));
            }
        }
        protected override void OnLoaded() {
            base.OnLoaded();
            if (Session.IsObjectToSave(this)) return;
            SaveOldValues();
        }
        protected override void OnSaved() {
            base.OnSaved();
            SaveOldValues();
        }
        private List<PropertyData> properties;
        public List<PropertyData> Properties { get { return properties; } }
        public Type Type { get { return this.GetType(); } }
    }
}
