var URL_GetPhns = URL_Base + "/GetEmployeePhones";
var phnDS;
var phnTable;
var PhnRecCnt = 0;


// ********************************************************************************
// Load Employee Phone Table
// ********************************************************************************
function LoadEmployeePhoneTable(e) {

	timeout = 40000;		// Set connection timeout
	URL = URL_GetPhns;


	empTable = $('#PhoneTable');

	empTable.jqGrid({
		colNames: ["Type", "Number", "Ext", "Prm"],
		colModel: [
			{ name: 'TypeDescr', index: 'TypeDescr', width: '40px', sorttype: "string" },
			{ name: 'PhoneNumber', index: 'PhoneNumber', width: '80px', sorttype: "string" },
			{ name: 'Extension', index: 'Extension', width: '40px', sorttype: "string" },
			{ name: 'IsPrimary', index: 'IsPrimary', width: '20px', sorttype: "string" }
		],
		rownumbers: true,
		reload: true,
		scroll: true,
		jsonReader: { cell: "" },
		rowNum: 10,
		rowList: [5, 10, 20, 50],
		sortname: 'PhoneNumber',
		sortorder: 'asc',
		height: "300px",
		caption: "Phones"
	});


	// --------------------------------------------------------------------------------
	// On Success function - Formats filtered data as table rows/columns
	// --------------------------------------------------------------------------------
	var Success = function (result) {

		var id;
		var newRow;

		PhnRecCnt = 0;
		phnDS = result.GetEmployeeListResult;

		if ($(UsersDD + ' option:selected').val() >= 0) {

			if (phnDS) {

				var mydata = [];

				for (var i = 0; i < phnDS.length; i++) {

					mydata[i] = {};
					mydata[i]["TypeDescr"] = (phnDS[i].TypeDescr ? phnDS[i].TypeDescr : "");
					mydata[i]["PhoneNumber"] = (phnDS[i].PhoneNumber ? phnDS[i].PhoneNumber : "");
					mydata[i]["Extension"] = (phnDS[i].Extension ? phnDS[i].Extension : "");
					mydata[i]["IsPrimary"] = (phnDS[i].IsPrimary ? phnDS[i].IsPrimary : "");

					++PhnRecCnt;
				}

				empTable.jqGrid('addRowData', PhnRecCnt, mydata);

				empTable.jqGrid('navGrid', '#EmpScroll',
								{ add: false, edit: false, del: false, search: true, refresh: true },
								{}, {}, {}, { multipleSearch: true, multipleGroup: true, showQuery: true })

				$(MsgBoxID).html(spanInfo + EmpRecCnt + ' Employees were found' + endSpan);

			} else {
				var errMsg = result.MessageID;
				if (errMsg)
					$(MsgBoxID).html(spanError + result.MessageString + endSpan);
				else
					$(MsgBoxID).html(spanError + "Unknown Error while querying Phones" + endSpan);
			}
		}
	};


	// --------------------------------------------------------------------------------
	// Local Error handler - resets local state
	// --------------------------------------------------------------------------------
	var Err = function (jqXHR, exception) {
		GlobErr(jqXHR, exception);
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
