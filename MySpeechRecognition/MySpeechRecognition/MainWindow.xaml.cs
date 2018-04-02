using Microsoft.Kinect;
using System;
using System.Globalization;
using System.IO;
using System.Speech.Recognition;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MySpeechRecognition
{
	public partial class MainWindow : Window
	{
		SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
		KinectSensor _sensor;
		WriteableBitmap colorBitmap;

		public MainWindow ()
		{
			InitializeComponent();
		}

		private void btnEnable_Click ( object sender, RoutedEventArgs e )
		{
			recEngine.RecognizeAsync(RecognizeMode.Multiple);
			buttonEnable();
		}

		private void buttonEnable ()
		{
			btnDisable.IsEnabled = true;
			btnEnable.IsEnabled = false;
		}

		private void WindowLoaded ( object sender, RoutedEventArgs e )
		{
			Choices commands = new Choices();
			commands.Add(new string[]
				{
					"Enable",
					"Disable",
					"clear",
					"Tilt 0 degree",
					"Tilt 10 degree",
					"Tilt 20 degree",
					"Tilt 27 degree",
					"take a picture"
				});
			GrammarBuilder gb = new GrammarBuilder();
			gb.Append(commands);
			Grammar grammer = new Grammar(gb);

			recEngine.LoadGrammarAsync(grammer);
			recEngine.SetInputToDefaultAudioDevice();
			recEngine.SpeechRecognized += recEngine_SpeechRecognized;
			recEngine.RecognizeAsync(RecognizeMode.Multiple);

			Window_Loaded_Kinect();
		}

		private void recEngine_SpeechRecognized ( object sender, SpeechRecognizedEventArgs e )
		{
			switch ( e.Result.Text )
			{
				case "Enable":
					buttonEnable();
					break;
				case "Disable":
					buttonDisable();
					break;
				case "Tilt 10 degree":
					_sensor.ElevationAngle = Convert.ToInt32("10");
					break;
				case "Tilt 0 degree":
					_sensor.ElevationAngle = Convert.ToInt32("0");
					break;
				case "Tilt 27 degree":
					_sensor.ElevationAngle = Convert.ToInt32("27");
					break;
				case "Tilt 20 degree":
					_sensor.ElevationAngle = Convert.ToInt32("20");
					break;
				case "clear":
					ClearCapturedImage();
					break;
				case "take a picture":
					TakePicture();
					break;
			}
		}

		private void btnDisable_Click ( object sender, RoutedEventArgs e )
		{
			buttonDisable();
		}

		private void buttonDisable ()
		{
			recEngine.RecognizeAsyncStop();
			btnEnable.IsEnabled = true;
		}

		// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		// Camera
		private void Window_Loaded_Kinect ()
		{
			if ( KinectSensor.KinectSensors.Count > 0 )
			{
				//Could be more than one just set to the first
				_sensor = KinectSensor.KinectSensors[0];

				//Check the State of the sensor
				if ( _sensor.Status == KinectStatus.Connected )
				{
					//Enable the features
					_sensor.ColorStream.Enable();
					_sensor.DepthStream.Enable();
					_sensor.SkeletonStream.Enable();
					_sensor.AllFramesReady += _sensor_AllFramesReady;
					//Double Tab
					// Start the sensor!

					try
					{
						_sensor.Start();
					}
					catch ( IOException )
					{
						_sensor = null;
					}
				}

				if ( _sensor.Status == KinectStatus.Connected )
				{
					colorBitmap = new WriteableBitmap
						(_sensor.ColorStream.FrameWidth,
						_sensor.ColorStream.FrameHeight,
						96.0, 96.0, PixelFormats.Bgr32, null);
				}
			}
		}

		void _sensor_AllFramesReady ( object sender, AllFramesReadyEventArgs e )
		{
			//using - Automatically dispose of the open when complete
			using ( ColorImageFrame colorFrame = e.OpenColorImageFrame() )
			{
				if ( colorFrame == null )
				{
					return;
				}
				byte[] pixels = new byte[colorFrame.PixelDataLength];
				colorFrame.CopyPixelDataTo(pixels);

				int stride = colorFrame.Width * 4;
				image1.Source = BitmapSource.Create(colorFrame.Width, colorFrame.Height, 96, 96, PixelFormats.Bgr32, null, pixels, stride);

				colorBitmap.WritePixels(
					new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
					pixels, this.colorBitmap.PixelWidth * sizeof(int), 0);
			}

			//throw new NotImplementedException();
		}

		private void Button_Click_1 ( object sender, RoutedEventArgs e )
		{
			TakePicture();
		}

		private void TakePicture ()
		{
			if ( null == _sensor )
			{
				return;
			}

			BitmapEncoder encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(colorBitmap));

			string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);
			string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			string path = System.IO.Path.Combine(myPhotos, "KinectSnapshot-" + time + ".png");

			try
			{
				using ( FileStream fs = new FileStream(path, FileMode.Create) )
				{
					encoder.Save(fs);
				}
			}
			catch ( IOException )
			{

				throw;
			}


			// display the saved image to screen
			var uri = new Uri(path);
			var bitmap = new BitmapImage(uri);
			capturedImage.Source = bitmap;
		}

		private void Button_Click_2 ( object sender, RoutedEventArgs e )
		{
			//makes sure the value is < 27 && > -27
			if ( txtTilt.Text != string.Empty )
			{
				int txtTiltValue = int.Parse(txtTilt.Text);
				if ( txtTiltValue <= 27 && txtTiltValue >= -27 )
				{
					_sensor.ElevationAngle = Convert.ToInt32(txtTilt.Text);
				}
			}
		}

		private void Clear_Button_Click ( object sender, RoutedEventArgs e )
		{
			ClearCapturedImage();
		}

		private void ClearCapturedImage ()
		{
			capturedImage.Source = null;
		}
	}
}
