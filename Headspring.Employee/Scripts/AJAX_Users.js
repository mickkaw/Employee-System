var URL_GetUsers = URL_Base + "/GetUserList";
var UsersDD = "#Users";
var userDS;


// ********************************************************************************
// Set Filter Box to Display data whenever there is input
// ********************************************************************************
$(document).ready(function () {
	LoadUserDropdown();
	$(UsersDD).change(DisplayPage);
});


// ********************************************************************************
//	Display page according to what Role the user plays
// ********************************************************************************
function DisplayPage(e) {

	var idx = $(UsersDD + ' option:selected').val();
	if (idx == -1) {
		$(MsgBoxID).html(spanError + "Please select a User" + endSpan);
	} else {
		if (userDS[idx].RoleID == 1) {

			// User is a regular employee - allow editing of some of their data
			LoadLocationsDropdown();
			LoadJobsDropdown();
			$("#EmpListPanel").css("display", "none");		// Remove the Employee List
			$("#EmpEditor").css("display", "inline");		// Remove the Employee Editor
			var empID = userDS[idx].EmpID;

		} else if (userDS[idx].RoleID == 3) {
			// User is an administrator - allow them to edit any employee; all data
			LoadLocationsDropdown();
			LoadJobsDropdown();
			LoadEmployeeTable();							// Load the Employee Table
			$("#EmpListPanel").css("display", "inline");	// Show the Employee List
			$("#EmpEditor").css("display", "inline");		// Remove the Employee Editor
		} else {
			// This user is not a valid user
			$("#EmpListPanel").css("display", "none");		// Remove the Employee List
			$("#EmpEditor").css("display", "none");			// Remove the Employee Editor
		}
	}
}


// ********************************************************************************
// Load User Dropdown
// ********************************************************************************
function LoadUserDropdown(e) {

	timeout = 60000;		// Set connection timeout
	URL = URL_GetUsers;


	// --------------------------------------------------------------------------------
	// On Success function - Formats filtered data as table rows/columns
	// --------------------------------------------------------------------------------
	var Success = function (result) {

		var id;
		var count = 0;
		var newOpt = "";

		$(UsersDD).find("option").remove(); // Remove all options

		userDS = result.GetUserListResult;
		if (userDS) {
		    count = userDS.length;

		    newOpt = '<option value="-1">Select ...</option>';
		    if (count == 0) {
		        var errMsg = result.MessageID;
		    } else for (var i = 0; i < count; i++) {
		        newOpt += '<option value="' + i.toString() + '">' + userDS[i].UserName + '</option>';
		    }

		    $(UsersDD).append(newOpt);
		}
	}


	// --------------------------------------------------------------------------------
	// Local Error handler - resets local state
	// --------------------------------------------------------------------------------
	var Err = function (jqXHR, exception) {
		GlobErr(jqXHR, exception);
		//$("#LoadingEmps").html(spanClear + endSpan);
	}


	// --------------------------------------------------------------------------------
	// AJAX Call to Load Data
	// --------------------------------------------------------------------------------
	var req = $.ajax({

		type: "GET"			// This is a Retrieval operation - Use GET here and 
		, url: URL			// URL established above; either GetProviderList
		, contentType: "application/json; charset=utf-8" // Webservice is set up to return JSON
		, dataType: "jsonp" // use jsonp to allow crossdomain calls; wraps the json string in a script
		, timeout: timeout	// *** This is important:  Errors would not fire when using "jsonp"
		//     if timeout was not specified
		, success: Success	// Run the Success function if connection is made
		, error: Err		// Run the Err function if something went wrong

	}); // var req = $.ajax
}
