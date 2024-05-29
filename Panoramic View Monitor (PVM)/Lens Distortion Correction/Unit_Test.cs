  //Load Image
  Image Source_Image = Image.FromFile("Distortion.jpg");

  // Example distortion coefficients
  double[] distCoeffs = { 0, 0, 0, 0, 0 };
  distCoeffs[0] = double.Parse(P_0.Text);
  distCoeffs[1] = double.Parse(P_1.Text);
  distCoeffs[2] = double.Parse(P_2.Text);
  distCoeffs[3] = double.Parse(P_3.Text);
  distCoeffs[4] = double.Parse(P_4.Text);
  
  // Example camera matrix 
  double[,] cameraMatrix = {
      { 1600, 0, src.Width},
      { 0, 1600, src.Height },
      { 0, 0, 1 }
  };
  
  // Correct the distortion
  Undistort(Source_Image, Destination_Image, cameraMatrix, distCoeffs);
  
  // Display the corrected image
  Corrected_Image.Image = Destination_Image;
  Corrected_Image.SizeMode = PictureBoxSizeMode.Zoom;
