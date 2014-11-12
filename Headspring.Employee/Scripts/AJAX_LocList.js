var URL_GetLocs = URL_Base + "/GetLocationList";
var LocsDD = "#Location";
var locDS;


// ********************************************************************************
// Load Locations Dropdown
// ********************************************************************************
function LoadLocationsDropdown(e) {

	timeout = 20000;		// Set connection timeout
	URL = URL_GetLocs;


	// --------------------------------------------------------------------------------
	// On Success function - Formats filtered data as table rows/columns
	// --------------------------------------------------------------------------------
	var Success = function (result) {

		var id;
		var count = 0;
		var newOpt = "";

		$(LocsDD).find("option").remove(); // Remove all options

		locDS = result.GetLocationListResult;
		count = locDS.length;

		newOpt = '<option value="-1">Select ...</option>';
		if (count == 0) {
			var errMsg = result.MessageID;
		} else for (var i = 0; i < count; i++) {
			newOpt += '<option value="' + i.toString() + '">' + locDS[i].LocationDescr + '</option>';
		}

		$(LocsDD).append(newOpt);
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
