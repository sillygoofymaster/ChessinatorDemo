using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
namespace ChessinatorDomain;
public class RequiredWithMessageAttribute : RequiredAttribute
{
    public RequiredWithMessageAttribute()
    {
        ErrorMessage = "Це поле є обов'язковим.";
    }
}

