using System;
using System.Collections.Generic;
using DAL;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Department_Management_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetCSVFromFile_InvaildValue_IsNull()
        {
            DepartmentDAL department = new DepartmentDAL();

            List<DataFromFile> data = department.LoadCSV("test");

            Assert.IsNull(data);
        }

        [TestMethod]
        public void GetCSVFromFile_VaildValue_IsNotNull()
        {
            DepartmentDAL department = new DepartmentDAL();

            List<DataFromFile> data = department.LoadCSV("dw");

            Assert.IsNull(data);
        }

    }
}
