using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sportify.Models
{
    public class Users
    {

public int Id { get; set; }

public int Dni { get; set; }

public string Name { get; set; }

public string Mail { get; set; }

public int Phone { get; set; }

public string Address { get; set; }

        
    }
}