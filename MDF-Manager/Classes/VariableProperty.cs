﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace MDF_Manager.Classes
{
    public class Float
    {
        public float data { get; set; }
        public Float(float fData)
        {
            data = fData;
        }
    }
    public class Float4 : INotifyPropertyChanged
    {
        private Color _mColor;
        private Brush _Brush;
        private float _X;
        private float _Y;
        private float _Z;
        private float _W;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void UpdateBrush()
        {
            byte[] hexArray = { _mColor.R, _mColor.G, _mColor.B };
            string hexBrush = "#" + BitConverter.ToString(hexArray).Replace("-", "");
            mBrush = HelperFunctions.GetBrushFromHex(hexBrush);
            OnPropertyChanged("mBrush");
        }
        public void UpdateColor()
        {
            _mColor.ScR = HelperFunctions.Clamp(x, 0, 1);
            _mColor.ScG = HelperFunctions.Clamp(y, 0, 1);
            _mColor.ScB = HelperFunctions.Clamp(z, 0, 1);
            _mColor.ScA = HelperFunctions.Clamp(w, 0, 1);
            UpdateBrush();
        }
        public float x { get => _X; set { _X = value; UpdateColor(); OnPropertyChanged("x"); } }
        public float y { get => _Y; set { _Y = value; UpdateColor(); OnPropertyChanged("y"); } }
        public float z { get => _Z; set { _Z = value; UpdateColor(); OnPropertyChanged("z"); } }
        public float w { get => _W; set { _W = value; UpdateColor(); OnPropertyChanged("w"); } }
        public Color mColor { get { return _mColor; } set { _mColor = value; UpdateBrush(); } }
        public Brush mBrush { get { return _Brush; } set { _Brush = value; } }
        public Float4(float fX, float fY, float fZ, float fW)
        {
            x = fX;
            y = fY;
            z = fZ;
            w = fW;
        }

    }
    public interface IVariableProp
    {
        string name { get; set; }
        object value { get; set; }
        int GetSize();
    }
    public class FloatProperty : IVariableProp
    {
        private string _Name;
        private Float _Default;
        public string name { get => _Name; set => _Name = value; }
        public object value { get => _Default; set => _Default = (Float)value; }
        public FloatProperty(string Name, Float Value)
        {
            name = Name;
            value = Value;
        }
        public int GetSize()
        {
            return 4;
        }
    }
    public class Float4Property : IVariableProp, INotifyPropertyChanged
    {
        private string _Name;
        private Float4 _Default;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string name { get => _Name; set => _Name = value; }
        public object value { get => _Default; set { _Default = (Float4)value; OnPropertyChanged("value"); _Default.UpdateColor(); } }
        public int[] indexes = new int[2];

        public Float4Property(string Name, Float4 Value, int matIndex, int propIndex)
        {
            name = Name;
            value = Value;
            indexes[0] = matIndex;
            indexes[1] = propIndex;
        }
        public int GetSize()
        {
            return 16;
        }
    }
}
