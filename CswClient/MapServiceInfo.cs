using System;
using System.Collections.Generic;
using System.Text;

namespace pdok4arcgis
{
  public class MapServiceInfo
  {
    private string _server;
    private string _service;
    private string _serviceType;
    private string _serviceParam;
    private bool _isSecured;

    public MapServiceInfo()
    {
      _server = "";
      _service = "";
      _serviceType = "";
      _serviceParam = "";
      _isSecured = false;
    }

    public string Server
    {
      get { return _server; }
      set { _server = value; }
    }

    public string Service
    {
      get { return _service; }
      set { _service = value; }
    }

    public string ServiceType
    {
      get { return _serviceType; }
      set { _serviceType = value; }
    }

    public string ServiceParam
    {
      get { return _serviceParam; }
      set { _serviceParam = value; }
    }

    public bool IsSecured
    {
      get { return _isSecured; }
      set { _isSecured = value; }
    }

    public override string ToString()
    {
      string str;

      str = "Server: " + _server + "; " + "Service: " + _service + "; " + "ServiceType: " + _serviceType + "; " +
            "ServiceParam: " + _serviceParam + "; " + "IsSecured: " + _isSecured.ToString();
      return str;
    }
  }
}
