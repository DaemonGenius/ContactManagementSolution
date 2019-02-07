$(document).ready(function () {
    contactDetailsList();
});
function updateClick() {
}
function addClick() {
}

function contactDetailsList() {
    $.ajax({
        url: '/api/ContactDetails/',
        type: 'GET',
        dataType: 'json',
        success: function (ContactDetails) {
            contactListSuccess(ContactDetails);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function contactListSuccess(ContactDetails) {
    $.each(ContactDetails, function (index, contact) {
        contactAddRow(contact);
    });
}

function contactAddRow(contacts) {
    if ($("#ContactTable tbody").length === 0) {
        $("#ContactTable").append("<tbody></tbody>");

    }
    $("#ContactTable tbody").append(
        contactBuildTableRow(contacts));
}

function contactBuildTableRow(contact) {
    var ret =

        "<tr>" +
        "<td>" +
        "<button type='button' " +
        "onclick='contactGet(this);' " +
        "class='btn btn-default' " +
        "data-id='" + contact.ID + "'>" +
        "<span class='glyphicon glyphicon-pencil' />"
        + "</button>" +
        "</td >" +
        "<td>" + contact.FName + "</td>" +
        "<td>" + contact.LName + "</td>"
        + "<td>" + contact.PhoneNumber + "</td>" +
        "<td>" +
        "<button type='button' " +
        "onclick='contactDelete(this);' " +
        "class='btn btn-default' " +
        "data-id='" + contact.ID + "'>" +
        "<span class='glyphicon glyphicon-remove-circle' />" +
        "</button>" +
        "</td>" +
        "</tr>";
    return ret;
}
function contactDelete(ctl) {
    var id = $(ctl).data("id");

    $.ajax({
        url: "/api/ContactDetails/" + id,
        type: 'DELETE',
        success: function (contact) {
            $(ctl).parents("tr").remove();
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function handleException(request, message,
    error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON !== null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}
function addClick() {
    Contact = new Object();
    Contact.FName = $("#fname").val();
    Contact.LName = $("#lName").val();
    Contact.PhoneNumber = $("#cNum").val();
    //Validation
    //Check if Fname & Lname is empty
    //Check CellNumber only numerical numbers
    //Check If cNum is a valid CellNumber with a reggex
    var x, num = /^\d{10}$/;
    x = Contact.PhoneNumber;
    if (Contact.FName !== "" && Contact.LName !== "") {
        if (isNaN(x) || x < 0 || x === "") {
            alert("Error Please Fill in correct Number");
        } else {
            if (x.match(num)) {
                if ($("#updateButton").text().trim() ===
                    "Add") {
                    ContactAdd(Contact);
                    formClear();
                } else if ($("#updateButton").text().trim() ===
                    "Update") {
                    Contact.ID = $("#contactId").val();
                    contactUpdate(Contact);
                    formClear();
                }
            } else {
                alert("Error Please Fill in a 10 digit Number");
            }

        }
    } else {
        alert("Error : Please enter a First Name & Last Name");
    }
    
   
}

function ContactAdd(Contact) {
    $.ajax({
        url: "/api/ContactDetails/PostNewContact",
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(Contact),
        success: function (Contact) {
            contactAddSuccess(Contact);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function contactUpdate(Contact) {
    $.ajax({
        url: "/api/ContactDetails/Update",
        type: 'PUT',
        contentType:
        "application/json;charset=utf-8",
        data: JSON.stringify(Contact),
        success: function (Contact) {
            contactUpdateSuccess(Contact);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function contactAddSuccess(Contact) {
    contactAddRow(Contact);
    formClear();
}
function contactUpdateSuccess(contact) {
    contactUpdateInTable(contact);
}
function formClear() {
    $("#fname").val("");
    $("#lName").val("");
    $("#cNum").val("");
}
function contactToFields(contact) {
   
    $("#fname").val(contact.FName);
    $("#lName").val(contact.LName);
    $("#cNum").val(contact.PhoneNumber);
}

function SearchClick() {
    var name;
    name = $("#fname").val();
    $.ajax({
        url: "/api/ContactDetails/" + name,
        type: 'GET',
        dataType: 'json',
        success: function (contact) {
            contactToFields(contact);
        },

        error: function (request, message, error) {
            handleException(request, message, error);
            alert("Not found");
        }
    });
}

function contactGet(ctl) {
    var id = $(ctl).data("id");
    $("#contactId").val(id);
    $.ajax({
        url: "/api/ContactDetails/" +id,
        type: 'GET',
        dataType: 'json',
        success: function (contact) {
            contactToFields(contact);          
            $("#updateButton").text("Update");
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}
function contactUpdateInTable(contact) {
    var row = $("#ContactTable button[data-id='" +
        contact.ID + "']").parents("tr")[0];
    $(row).after(contactBuildTableRow(contact));
    $(row).remove();
    formClear();
    $("#updateButton").text("Add");
}
