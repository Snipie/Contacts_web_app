namespace Contacts.Controllers;
using Microsoft.AspNetCore.Mvc;
using Contacts.Models;
using System.Diagnostics;
using System.Linq;

public class ContactController : Controller
{
    public IActionResult All()
    {
        // Get all the stored contacts.
        // Return all the saved contacts.
        ViewData["Contacts"] = Contact.GetAll();
		
        return View("/Views/Home/Index.cshtml");
    }

	[HttpPost]
    public IActionResult Create(Contact c)
    {
		Debug.WriteLine("Inside Create controller");
        // Persist the contact information.
		Contact.Save(c);
		ViewData["Contacts"] = Contact.GetAll();
		
        return View("/Views/Home/Index.cshtml");
    }

	[HttpGet]
	public IActionResult Edit(int id)
    {
        Contact model = Contact.GetAll().Where(c => c.ID == id).FirstOrDefault();
		
		
        return View("Edit", model);
    }

	[HttpPost]
    public IActionResult Edit(Contact c)
    {
        // Modify the contact information.
		Contact.Edit(c);
		ViewData["Contacts"] = Contact.GetAll();
		
        return View("/Views/Home/Index.cshtml");
    }

    public IActionResult Delete(Contact c)
    {
        // Persist the contact information.
		Contact.Delete(c);
		ViewData["Contacts"] = Contact.GetAll();
		
        return View("/Views/Home/Index.cshtml");
    }
}