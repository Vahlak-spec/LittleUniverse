using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : MonoBehaviour
{
    [SerializeField] private Transform _viewPoint;

    private List<FollowData> _followData = new List<FollowData>();


    public void AddFollow(FollowTarget target, FollowItem followItem)
    {
        if (followItem.IsFollow) return;

        followItem.IsFollow = true;
        _followData.Add(new FollowData(followItem, _viewPoint, target.Transform, target.GetSeconds));
    }

    private void FixedUpdate()
    {
        foreach (var item in _followData)
        {
            item.OnFixed();
        }
    }

    private class FollowData
    {
        private FollowItem _followItem;
        private Transform _targetPoint;
        private PointData[] _pointDatas;
        private PointData _tempPoint;
        private int _curPoint;

        public FollowData(FollowItem followItem, Transform viewTransform, Transform target, float second)
        {
            _followItem = followItem;
            _targetPoint = target;
            _pointDatas = new PointData[(int)(second / Time.fixedDeltaTime)];

            for (int i = 0; i < _pointDatas.Length; i++)
            {
                _pointDatas[i] = new PointData(viewTransform);
                _pointDatas[i].CashPoint(target);
            }
            _tempPoint = new PointData(viewTransform);
        }

        public void OnFixed()
        {
            _tempPoint.CashPoint(_targetPoint);

            if (!_tempPoint.Equals(_pointDatas[_curPoint > 0 ? _curPoint - 1 : _pointDatas.Length - 1]))
            {
                _pointDatas[_curPoint].Copy(_tempPoint);
                _curPoint = _curPoint < _pointDatas.Length - 1 ? _curPoint + 1 : 0;

                _pointDatas[_curPoint].SetPoint(_followItem);
            }
            else
            {
                _followItem.SetIsRun(false);
            }
        }

        private class PointData
        {
            public Vector3 Position => _position;
            public Vector3 Angle => _angle;

            private Vector3 _position;
            private Vector3 _angle;
            private Transform _viewTransform;
            public void Copy(PointData pointData) 
            {
                _position = pointData.Position;
                _angle = pointData.Angle;
            }
            public bool Equals(PointData pointData)
            {
                return _position == pointData.Position && _angle == pointData.Angle;
            }
            public PointData(Transform viewTransform)
            {
                _viewTransform = viewTransform;
            }
            public void CashPoint(Transform transform)
            {
                _viewTransform.position = transform.position;
                _viewTransform.eulerAngles = transform.eulerAngles;

                _position = _viewTransform.localPosition;
                _angle = _viewTransform.localEulerAngles;
            }
            public void SetPoint(FollowItem followItem)
            {
                if (followItem.Transform.localPosition == _position && followItem.Transform.localEulerAngles == _angle)
                {
                    followItem.SetIsRun(false);
                    return;
                }

                followItem.SetIsRun(true);

                followItem.Transform.localPosition = _position;
                followItem.Transform.localEulerAngles = _angle;
            }
        }
    }
}
