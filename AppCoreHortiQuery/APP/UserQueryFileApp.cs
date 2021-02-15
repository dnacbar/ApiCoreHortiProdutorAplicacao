﻿using APPCOREHORTIQUERY.IAPP;
using APPDTOCOREHORTIQUERY.SIGNATURE;
using System.IO;
using System.Threading.Tasks;

namespace APPCOREHORTIQUERY.APP
{
    public class UserQueryFileApp : IUserQueryFileApp
    {
        public async Task<byte[]> GetUserImage(ConsultUserSignature signature)
        {
            byte[] imageFile;
            if (signature.IsProducer)
            {
                if (!File.Exists(Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory()), "PRODUCER", signature.Id.ToString(), signature.Id + ".jpeg")))
                    return null;

                imageFile = await File.ReadAllBytesAsync(Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory()),
                                                                      "PRODUCER", signature.Id.ToString(), signature.Id + ".jpeg"));
            }
            else
            {
                if (!File.Exists(Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory()), "CLIENT", signature.Id.ToString(), signature.Id + ".jpeg")))
                    return null;

                imageFile = await File.ReadAllBytesAsync(Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory()),
                                                                      "CLIENT", signature.Id.ToString(), signature.Id + ".jpeg"));
            }

            return imageFile;
        }
    }
}