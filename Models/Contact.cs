namespace Contacts.Models;
using System.Text;

public class Contact
{
	public static string filename = "contacts.xml";
	
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool HasWhatsAppAccount { get; set; }
    public bool HasTelegramAccount { get; set; }
    public string? Notes { get; set; }

    public static List<Contact> GetAll()
	{
		// Check if the file exists.
		// Create the file if it does not exist.
		FileInfo fi = new FileInfo(filename);
		if(!fi.Exists)
			fi.Create();
		
		// Read the file.
		// If the file is empty, return an empty list.
		FileStream fs = new FileStream(filename, FileMode.Open);
		if(fs.Length == 0)
		{
			fs.Close();
			return new List<Contact>();
		}
		
		System.Xml.Serialization.XmlSerializer contactXmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Contact>));
		List<Contact> contacts = (List<Contact>) contactXmlSerializer.Deserialize(fs);
		fs.Close();
		fs.Dispose();
			
		return contacts;
	}
	
	public static void SaveAll(List<Contact> contacts)
	{
		//System.Xml.Serialization.XmlSerializer contactXmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Contact>));
		//TextWriter writer = new StreamWriter(filename);
		//contactXmlSerializer.Serialize(writer, contacts);
		//writer.Close();
		//writer.Dispose();
		
		
		StreamWriter streamWriter = new StreamWriter(filename);
		System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(streamWriter, new() 
		{
			Encoding = Encoding.UTF8,
			Indent = true
		});
		System.Xml.Serialization.XmlSerializer contactXmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Contact>));
		contactXmlSerializer.Serialize(xmlWriter, contacts);
		streamWriter.Close();
	}

	public static void Save(Contact model)
	{
		List<Contact> contacts = GetAll();
		model.ID = contacts.Count + 1;
		contacts.Add(model);
		SaveAll(contacts);
	}

	public static void Delete(Contact model)
	{
		List<Contact> contacts = GetAll();
		contacts = contacts.Except(contacts.Where(c => c.ID == model.ID).ToList()).ToList();
		SaveAll(contacts);
	}

	public static void Edit(Contact model)
	{
		List<Contact> contacts = GetAll();
		contacts = contacts.Except(contacts.Where(c => c.ID == model.ID).ToList()).ToList();
		contacts.Add(model);
		SaveAll(contacts);
	}
	
	public override string ToString()
	{
		return "ID: " + this.ID + " Name: " + this.Name + " Phone: " + this.Phone + " Email: " + this.Email + " HasWhatsAppAccount: " +
		this.HasWhatsAppAccount + " HasTelegramAccount:" + this.HasTelegramAccount + " Notes: " + this.Notes;
	}
}