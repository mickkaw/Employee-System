var URL_GetJobs = URL_Base + "/GetJobList";
var JobsDD = "#JobCode";
var JobDS;

// ********************************************************************************
// Load Jobs Dropdown
// ********************************************************************************
function LoadJobsDropdown(e) {

	timeout = 20000;		// Set connection timeout
	URL = URL_GetJobs;


	// --------------------------------------------------------------------------------
	// On Success function - Formats filtered data as table rows/columns
	// --------------------------------------------------------------------------------
	var Success = function (result) {

		var id;
		var count = 0;
		var newOpt = "";

		$(JobsDD).find("option").remove(); // Remove all options

		jobDS = result.GetJobListResult;
		count = jobDS.length;

		newOpt = '<option value="-1">Select ...</option>';
		if (count == 0) {
			var errMsg = result.MessageID;
		} else for (var i = 0; i < count; i++) {
			newOpt += '<option value="' + i.toString() + '">' + jobDS[i].JobCode + '</option>';
		}

		$(JobsDD).append(newOpt);	// Add list of options to the DropDown
	}


	// --------------------------------------------------------------------------------
	// Local Error handler - resets local state
	// --------------------------------------------------------------------------------
	var Err = function (jqXHR, exception) {
		GlobErr(jqXHR, exception);
		//$("#LoadingEmps").html(spanClear + endSpan);
	}


	var req = $.ajax({

		type: "GET"			// This is a Retrieval operation - Use GET here and 
		, url: URL			// URL established above; either GetProviderList
		, contentType: "application/json; charset=utf-8" // Webservice is set up to return JSON
		, dataType: "jsonp" // use jsonp to allow crossdomain calls; wraps the json string in a script
		, timeout: timeout	// *** This is important:  Errors would not fire when using "jsonp"
		//     if timeout was not specified
		, success: Success	// Run the Success function if connection is made
		, error: Err	// Run the Err function if something went wrong

	}); // var req = $.ajax
}
