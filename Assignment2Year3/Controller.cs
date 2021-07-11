using System.Windows.Forms;
using System.Windows.Input;
using Model;
using View;

namespace Assignment2Year3
{
    class Controller
    {
        //Declare Image storing container
        IPictureInterface _pictureInterface;
        
        //Declare a Image updater
        private IImageUpdator _imageUpdator;
        //Decalare a scaler for scaling images
        private IScaler _scaler;
        //Declare an ImageDisplay form
        private ImageDisplay imageDisplay;

        public Controller()
        {
            //Instantiate functional objects
            //Instatiate Scaler
            _scaler = new Scaler();
            //Instantiate Picture interface
            _pictureInterface = new PictureInterface();
            //Instantiate ImageUpdater
            _imageUpdator = new ImageUpdator(_pictureInterface, _scaler);
            //Instantiate ImageDisplay
            imageDisplay = new ImageDisplay(ExecuteCommand, _pictureInterface.LoadImageToDisplay, _pictureInterface.LoadSelectedImage);

            //Subsribe publishers
            (_imageUpdator as IEventPublisher).Subscribe(imageDisplay.OnNewInput);
            (_pictureInterface as IEventPublisher).Subscribe((_imageUpdator as IEventListener).OnNewInput);
            // (_pictureInterface as IEventPublisher).Subscribe((_imageUpdator as IEventListener).OnNewInput);


            //Start the application
            Application.Run(imageDisplay);
        }

        /// <summary>
        /// Implementation of ExecuteDelegate, for the command pattern
        /// </summary>
        /// <param name="command"></param>
        public void ExecuteCommand(ICommander command)
        {
            command.Execute();
        }
    }
}
