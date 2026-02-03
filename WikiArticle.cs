using System;

public class WikiArticle
{
    public string title { get; set; } // Τίτλος λήμματος
    public string extract { get; set; } // Η σύνοψη κειμένου
    public WikiImage thumbnail { get; set; } // Η εικόνα (αν υπάρχει)
}

public class WikiImage
{
    public string source { get; set; } // Το URL της εικόνας
}