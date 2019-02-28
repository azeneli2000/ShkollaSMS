using System;
using System.Collections.Generic;
using System.Text;
using java.util; 
using java.util.zip; 
using java.io;
using System.IO;

namespace SMS_Shkolla_Manager
{
    class ZippClass
    {
        public void Zip(string zipFileName, string[] sourceFile)
        {
            FileOutputStream filOpStrm = new FileOutputStream(zipFileName);
            ZipOutputStream zipOpStrm = new ZipOutputStream(filOpStrm);
            FileInputStream filIpStrm = null;
            foreach(string strFilName  in sourceFile)
            { 
                filIpStrm = new FileInputStream(strFilName);
                ZipEntry ze = new  ZipEntry(Path.GetFileName(strFilName)); 
                            zipOpStrm.putNextEntry(ze);
                sbyte[] buffer =  new
                sbyte[1024];
                int len =  0;
                while ((len = 

                 filIpStrm.read(buffer))  >= 0)
                {
                    zipOpStrm.write(buffer, 0, len);
                }
            }
            zipOpStrm.closeEntry();
            filIpStrm.close();
            zipOpStrm.close();
            filOpStrm.close();
        }

        public void Extract(string zipFileName, string destinationPath)
        {
            ZipFile zipfile = new ZipFile(zipFileName);
            List<ZipEntry> zipFiles = GetZipFiles(zipfile);

            foreach (ZipEntry zipFile in zipFiles)
            {
                if (!zipFile.isDirectory())
                {
                    InputStream s = zipfile.getInputStream(zipFile); 
                    try 
                    {
                        Directory.CreateDirectory(destinationPath + "\\"  + 
                            Path.GetDirectoryName(zipFile.getName()));
                        FileOutputStream dest = new 
                            FileOutputStream(Path.Combine(destinationPath + "\\" +
                            Path.GetDirectoryName(zipFile.getName()), 
                            Path.GetFileName(zipFile.getName()))); 
                        try
                        {
                            int len = 0;
                            sbyte[] buffer = new  sbyte[7168];
                            while ((len = s.read(buffer))  >= 0)
                            {
                                dest.write(buffer, 0, len);
                            }
                        }
                        finally
                        {
                            dest.close();
                        }
                    }
                    finally
                    {
                        s.close();
                    }
                }
            }
        }

        private List<ZipEntry> GetZipFiles(ZipFile zipfil)
        {
            List<ZipEntry> lstZip = new List<ZipEntry>();
            Enumeration zipEnum = zipfil.entries();
            while (zipEnum.hasMoreElements())
            {
                ZipEntry zip = (ZipEntry)zipEnum.nextElement();
                lstZip.Add(zip);
            }
            return lstZip;
        }
    }
}
