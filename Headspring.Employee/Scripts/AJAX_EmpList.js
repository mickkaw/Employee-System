var URL_GetEmps = URL_Base + "/GetEmployeeList";
var empDS;
var empTable;
var EmpRecCnt = 0;

var spanClear = '<span>';
var spanInfo = '<span style="color: #000099;">';
var spanError = '<span style="color: #990000;">';
var endSpan = '</span>';

var GridHt;

var waitGIFOpts = {
	lines: 13, // The number of lines to draw
	length: 20, // The length of each line
	width: 5, // The line thickness
	radius: 25, // The radius of the inner circle
	corners: 1, // Corner roundness (0..1)
	rotate: 0, // The rotation offset
	direction: 1, // 1: clockwise, -1: counterclockwise
	color: '#000099', // #rgb or #rrggbb or array of colors
	speed: 1, // Rounds per second
	trail: 60, // Afterglow percentage
	shadow: true, // Whether to render a shadow
	hwaccel: false, // Whether to use hardware acceleration
	className: 'spinner', // The CSS class to assign to the spinner
	zIndex: 2e9, // The z-index (defaults to 2000000000)
	top: '50%', // Top position relative to parent
	left: '50%' // Left position relative to parent
};

var waitRdFac = 12;
var waitRdLim = 7;
var waitLtFac = 20;
var waitLtLim = 7;
var waitWdFac = 50;
var waitWdLim = 5;
var waitLnFac = 20;
var waitLnLim = 7;


// ********************************************************************************
// Load Employee Table
// ********************************************************************************
function LoadEmployeeTable(e) {

	timeout = 60000;		// Set connection timeout
	URL = URL_GetEmps;


	empTable = $('#EmployeeTable');

	empTable.jqGrid({
		colNames: ["Last Name", "First Name", "Job Code", "Location"],
		colModel: [
			{ name: 'LastName', index: 'LastName', width: '95px', sorttype: "string" },
			{ name: 'FirstName', index: 'FirstName', width: '80px', sorttype: "string" },
			{ name: 'JobCode', index: 'JobCode', width: '40px', sorttype: "string" },
			{ name: 'LocationDescr', index: 'LocationDescr', width: '95px', sorttype: "string" }
		],
		rownumbers: true,
		reload: true,
		scroll: true,
		pager: '#EmpScroll',
		jsonReader: { cell: "" },
		rowNum: 10,
		rowList: [5, 10, 20, 50],
		sortname: 'LastName',
		sortorder: 'asc',
		height: "560px",
		caption: "Employee List"
	});


	// --------------------------------------------------------------------------------
	// On Success function - Formats filtered data as table rows/columns
	// --------------------------------------------------------------------------------
	var Success = function (result) {

		var id;
		var newRow;

		EmpRecCnt = 0;
		empDS = result.GetEmployeeListResult;

		if ($(UsersDD + ' option:selected').val() >= 0) {

			if (empDS) {

				var mydata = [];

				for (var i = 0; i < empDS.length; i++) {

					mydata[i] = {};
					mydata[i]["LastName"] = (empDS[i].LastName ? empDS[i].LastName : "");
					mydata[i]["FirstName"] = (empDS[i].FirstName ? empDS[i].FirstName : "") + (empDS[i].MiddleName ? " " + empDS[i].MiddleName : "");
					mydata[i]["MiddleName"] = (empDS[i].MiddleName ? empDS[i].MiddleName : "");
					mydata[i]["JobCode"] = (empDS[i].JobCode ? empDS[i].JobCode : "");
					mydata[i]["LocationDescr"] = (empDS[i].LocationDescr ? empDS[i].LocationDescr : "");

					++EmpRecCnt;
				}

				empTable.jqGrid('addRowData', EmpRecCnt, mydata);

				empTable.jqGrid('navGrid', '#EmpScroll',
								{ add: false, edit: false, del: false, search: true, refresh: true },
								{}, {}, {}, { multipleSearch: true, multipleGroup: true, showQuery: true })

				$(MsgBoxID).html(spanInfo + EmpRecCnt + ' Employees were found' + endSpan);

			} else {
				var errMsg = result.MessageID;
				if (errMsg)
					$(MsgBoxID).html(spanError + result.MessageString + endSpan);
				else
					$(MsgBoxID).html(spanError + "Unknown Error while querying Employees" + endSpan);
			}
		}

		$("#LoadingEmps").html(spanClear + endSpan);	// Clear Loading Message

		TurnControls(1);
	};


	// --------------------------------------------------------------------------------
	// Local Error handler - resets local state
	// --------------------------------------------------------------------------------
	var Err = function (jqXHR, exception) {
		GlobErr(jqXHR, exception);
		$("#LoadingEmps").html(spanClear + endSpan);
		TurnControls(1);
	}


	// --------------------------------------------------------------------------------
	// Before Send function - Clears messages and turns on Busy GIF 
	// --------------------------------------------------------------------------------
	var B4Send = function () {

		$(MsgBoxID).html("");
		$(MsgBoxID).show();						// Clear Error Message

		TurnControls(0);

		if (empTable) empTable.jqGrid('clearGridData').trigger("reloadGrid");

		if ($(UsersDD + ' option:selected').val() != -1) {

			GridHt = Math.floor($("#EmpListPanel").height());
			waitGIFOpts.radius = Math.floor(GridHt / waitRdFac < waitRdLim ? waitRdLim : GridHt / waitRdFac);
			waitGIFOpts.length = Math.floor(GridHt / waitLtFac < waitLtLim ? waitLtLim : GridHt / waitLtFac);
			waitGIFOpts.width = Math.floor(GridHt / waitWdFac < waitWdLim ? waitWdLim : GridHt / waitWdFac);
			waitGIFOpts.lines = Math.floor(GridHt / waitLnFac < waitLnLim ? waitLnLim : GridHt / waitLnFac);

			var spinner = new Spinner(waitGIFOpts).spin(document.getElementById('LoadingEmps'));
		}
	};


	// --------------------------------------------------------------------------------
	// Enable/Disable Page Controls
	// --------------------------------------------------------------------------------
	var TurnControls = function (status) {

		if (status != 0) {
			$('#NewEmp').removeAttr("disabled");
			$('#DelEmp').removeAttr("disabled");
			$('#SavEmp').removeAttr("disabled");
			$('#CanEmp').removeAttr("disabled");
			$('#AddPhn').removeAttr("disabled");
		} else {
			$('#NewEmp').attr("disabled", "disabled");
			$('#DelEmp').attr("disabled", "disabled");
			$('#SavEmp').attr("disabled", "disabled");
			$('#CanEmp').attr("disabled", "disabled");
			$('#AddPhn').attr("disabled", "disabled");
		}
	}


	// --------------------------------------------------------------------------------
	// AJAX Call to Load Data
	// --------------------------------------------------------------------------------
	var req = $.ajax({

		type: "GET"					// This is a Retrieval operation - Use GET here and 
		, url: URL					// URL established above; either GetProviderList
		, contentType: "application/json; charset=utf-8" // Webservice is set up to return JSON
		, dataType: "jsonp"			// use jsonp to allow crossdomain calls; wraps the json string in a script
		, timeout: timeout			// *** This is important:  Errors would not fire when using "jsonp"
		//     if timeout was not specified
		, success: Success			// Run the Success function if connection is made
		, error: Err				// Run the Err function if something went wrong
		, beforeSend: B4Send		// Turns on the Busy Icon

	}); // var req = $.ajax
}
