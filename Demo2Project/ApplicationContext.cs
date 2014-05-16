using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo2Project
{
  public static class ApplicationContext
  {
    public static Message Message
    {
      get
      {
        return (Message) HttpContext.Current.Session[@"Message"];
      }
      set
      {
        HttpContext.Current.Session[@"Message"] = value;
      }
    }
  }
}