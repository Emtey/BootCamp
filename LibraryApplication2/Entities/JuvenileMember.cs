using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    /// <summary>
    /// JuvenileMember class, inherits from Member
    /// </summary>
    public class JuvenileMember:Member
    {
        private Int16 _adultMemberID;
        private DateTime _birthDate;

        #region Properties
        /// <summary>
        /// gets/sets the AdultMemberID property
        /// </summary>
        public Int16 AdultMemberID
        {
            get { return _adultMemberID; }
            set
            {
                if (value > 0 && value <= 32767)
                    _adultMemberID = value;
                else
                    throw new ArgumentOutOfRangeException("Adult Member ID must be betwen 1 and 32767.");
            }
        }

        /// <summary>
        /// Gets/sets the BirthDate Property
        /// </summary>
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }
        #endregion

        /// <summary>
        /// defualt constructor for juvenileMember
        /// </summary>
        public JuvenileMember()
        {
            //AdultMemberID = 0;
            //BirthDate = DateTime.MinValue;
        }
    }
}
