using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirspaceUsage.AirspaceMap
{
    public class WarningAreaGroup : IEnumerable<MapWarningArea>
    {
        private List<MapWarningArea> groupMembers = new List<MapWarningArea>();

        public string GroupName;
        public string GroupDescription;

        public void AddGroupMember(MapWarningArea newMember)
        {
            groupMembers.Add(newMember);
        }

        public void RemoveMember(MapWarningArea member)
        {
            groupMembers.Remove(member);
        }

        public IEnumerator<MapWarningArea> GetEnumerator()
        {
            return groupMembers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return groupMembers.GetEnumerator();
        }

        public override string ToString()
        {
            return GroupName;
        }
    }
}
