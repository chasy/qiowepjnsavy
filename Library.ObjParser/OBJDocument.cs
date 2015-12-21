﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.ObjParser
{
    public class OBJDocument
    {
        public List<Vertex> VertexList;
        public List<Face> FaceList;
        public List<TextureVertex> TextureList;

        public Extent Size { get; set; }
        public string Mtl { get; set; }

        //public OBJDocument(List<Vertex> VertexList, List<Face> FaceList)
        //{
        //    this.VertexList = VertexList;
        //    this.FaceList = FaceList;
        //}

        /// <summary>
        /// Parse and load an OBJ file into memory.  Will consume memory
        /// at aproximately 120% the size of the file.
        /// </summary>
        /// <param name="path">path to obj file on disk</param>
        /// <param name="linesProcessedCallback">callback for status updates</param>
        public OBJDocument LoadObj(string path)
        {
            OBJDocument objDoc = new OBJDocument();

            VertexList = new List<Vertex>();
            FaceList = new List<Face>();
            TextureList = new List<TextureVertex>();

            var input = File.ReadLines(path);

            foreach (string line in input)
            {
                processLine(line);
            }
            objDoc.VertexList = VertexList;
            objDoc.FaceList = FaceList;
            objDoc.TextureList = TextureList;

            return objDoc;
            //updateSize();
        }

        private void WriteObjFile(string path)
        {
            using (var outStream = File.OpenWrite(path))
            using (var writer = new StreamWriter(outStream))
            {

                // Write some header data
                writer.WriteLine("# Generated by ObjParser");

                if (!string.IsNullOrEmpty(Mtl))
                {
                    writer.WriteLine("mtllib " + Mtl);
                }

                VertexList.ForEach(v => writer.WriteLine(v));
                TextureList.ForEach(tv => writer.WriteLine(tv));
                FaceList.ForEach(f => writer.WriteLine(f));
            }
        }

        /// <summary>
        /// Sets our global object size with an extent object
        /// </summary>
        private void updateSize()
        {
            Size = new Extent
            {
                XMax = VertexList.Max(v => v.X),
                XMin = VertexList.Min(v => v.X),
                YMax = VertexList.Max(v => v.Y),
                YMin = VertexList.Min(v => v.Y),
                ZMax = VertexList.Max(v => v.Z),
                ZMin = VertexList.Min(v => v.Z)
            };
        }

        /// <summary>
        /// Parses and loads a line from an OBJ file.
        /// Currently only supports V, VT, F and MTLLIB prefixes
        /// </summary>		
        private void processLine(string line)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "mtllib":
                        Mtl = parts[1];
                        break;
                    case "v":
                        Vertex v = new Vertex();
                        v.LoadFromStringArray(parts);
                        VertexList.Add(v);
                        v.Index = VertexList.Count();
                        break;
                    case "f":
                        Face f = new Face();
                        f.LoadFromStringArray(parts);
                        FaceList.Add(f);
                        break;
                    case "vt":
                        TextureVertex vt = new TextureVertex();
                        vt.LoadFromStringArray(parts);
                        TextureList.Add(vt);
                        vt.Index = TextureList.Count();
                        break;

                }
            }
        }
    }
}