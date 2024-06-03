        static void LDC(Bitmap src, Bitmap dst, double[,] cameraMatrix, double[] distCoeffs)
        {
            //Camera Parameters of Normalization
            double fx = cameraMatrix[0, 0];
            double fy = cameraMatrix[1, 1];
            double cx = cameraMatrix[0, 2];
            double cy = cameraMatrix[1, 2];

            //Get Parameters of Distortion
            double k1 = distCoeffs[0];
            double k2 = distCoeffs[1];
            double p1 = distCoeffs[2];
            double p2 = distCoeffs[3];
            double k3 = distCoeffs[4];

            //Start to set pixel
            for (int v = 0; v < src.Height; v++)
            {
                for (int u = 0; u < src.Width; u++)
                {

                    //Normalisation
                    double x = (u - cx) / fx;
                    double y = (v - cy) / fy;
                    //Radial Distortion
                    double r2 = x * x + y * y;
                    double radial_distortion = 1 + k1 * r2 + k2 * r2 * r2 + k3 * r2 * r2 * r2;
                    double x_distorted = x * radial_distortion + 2 * p1 * x * y + p2 * (r2 + 2 * x * x);
                    double y_distorted = y * radial_distortion + 2 * p2 * x * y + p1 * (r2 + 2 * y * y);

                    //Get The Position of Distorted Image
                    int u_distorted = (int)(fx * x_distorted + cx);
                    int v_distorted = (int)(fy * y_distorted + cy);

                    if (u_distorted >= 0 && u_distorted < src.Width && v_distorted >= 0 && v_distorted < src.Height)
                    {
                        //Set Pixel from Source Images
                        dst.SetPixel(u, v, src.GetPixel(u_distorted, v_distorted));
                    }
                    else
                    {
                        // Black for out-of-bound pixels
                        dst.SetPixel(u, v, Color.Black); 
                    }
                }
            }
        }
