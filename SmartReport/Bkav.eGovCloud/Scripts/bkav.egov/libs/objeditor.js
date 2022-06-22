
//Them truong tu custom dropdownlist
function OnClientCommandExecuting(editor, args)
{
    var name = args.get_name();
    var val = args.get_value();                       
    if (name == "DynamicDropdown")
    {
        editor.pasteHtml(val);                     
        args.set_cancel(true); 
    }
    if (name == "eDropDownCustom")
    {
        editor.pasteHtml(val);                     
        args.set_cancel(true); 
    }
 }     