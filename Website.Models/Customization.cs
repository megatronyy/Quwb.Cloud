﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Website.Models
{
    public class Customization
    {
        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.ID
         *
         * @mbg.generated
         */
        public int id { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.UserID
         *
         * @mbg.generated
         */
        public int userid { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.BusinessID
         *
         * @mbg.generated
         */
        public int businessid { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.LocationID
         *
         * @mbg.generated
         */
        public int locationid { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.MinPrice
         *
         * @mbg.generated
         */
        public int minprice { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.MaxPrice
         *
         * @mbg.generated
         */
        public int maxprice { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.MinArea
         *
         * @mbg.generated
         */
        public int minarea { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.MaxArea
         *
         * @mbg.generated
         */
        public int maxarea { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.Remark
         *
         * @mbg.generated
         */
        public string remark { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.CreateTime
         *
         * @mbg.generated
         */
        public DateTime createtime { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.UpdateTime
         *
         * @mbg.generated
         */
        public DateTime updatetime { get; set; }

        /**
         *
         * This field was generated by MyBatis Generator.
         * This field corresponds to the database column Customization.IsActive
         *
         * @mbg.generated
         */
        public short isactive { get; set; }
    }
}
