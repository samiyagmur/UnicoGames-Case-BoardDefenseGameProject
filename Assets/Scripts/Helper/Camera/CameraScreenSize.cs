using UnityEngine;

namespace Helper
{
    public class CameraScreenSize : MonoBehaviour
    {
        private float _referansXScreenSize;
        private float _referansYScreenSize;
        private float _changedXScreenSize;
        private float _changedYScreenSize;
        private float _multiplyOfXScreenSize;
        private float _multiplyOfYScreenSize;
        private float _multiplyScreenSize;

        private void Awake()
        {
           
            Init();
        }

        private void Init()
        {
            ArrangeSize();
        }

        private void ArrangeSize()
        {
            _referansXScreenSize = 1080;

            _referansYScreenSize = 1920;


            if (_referansXScreenSize == Camera.main.pixelWidth && _referansYScreenSize != Camera.main.pixelHeight) return;

            _changedXScreenSize = Camera.main.pixelWidth;

            _changedYScreenSize = Camera.main.pixelHeight;

            _multiplyOfXScreenSize = _referansXScreenSize / _changedXScreenSize;

            _multiplyOfYScreenSize = _referansYScreenSize / _changedYScreenSize;

            _multiplyScreenSize = _multiplyOfXScreenSize / _multiplyOfYScreenSize;

            if (_multiplyScreenSize > 0)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize * _multiplyScreenSize;
            }

            // cube.gameObject.transform.localScale = cube.gameObject.transform.localScale
            // multiply;Meaning Of that Line  adjusting to referanced object sclale for cam size;
        }


    }
}



