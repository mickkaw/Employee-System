var URL_Base = "http://localhost/Headspring.Employee.WebServices/EmpService";
var URL = "";
var MsgBoxID = "#ErrMsg";


// --------------------------------------------------------------------------------
// On Error function - Displays error messages based on returned status
// --------------------------------------------------------------------------------
var GlobErr = function (jqXHR, exception) {
	if (jqXHR.status === 0) {
		$(MsgBoxID).html(spanError + "<b>Error</b>: &nbsp;Unable to Connect - Verify WebService / Network." + endSpan);
	} else if (jqXHR.status === 404) {
		$(MsgBoxID).html(spanError + "<b>Error [404]</b>: &nbsp;Requested page not found." + endSpan);
	} else if (jqXHR.status === 500) {
		$(MsgBoxID).html(spanError + "<b>Error [500]</b>: &nbsp;Internal Server Error." + endSpan);
	} else if (exception === 'parsererror') {
		$(MsgBoxID).html(spanError + "<b>Error</b>: &nbsp;Requested JSON parse failed." + endSpan);
	} else if (exception === 'timeout') {
		$(MsgBoxID).html(spanError + "<b>Error</b>: &nbsp;Time out error." + endSpan);
	} else if (exception === 'abort') {
		$(MsgBoxID).html(spanError + "<b>Error</b>: &nbsp;Ajax request aborted." + endSpan);
	} else $(MsgBoxID).html(spanError + "<b>Error [" + jqXHR.status.toString() + "]</b>: &nbsp;" + jqXHR.responseText + endSpan);

	$(MsgBoxID).show();		// Show Error Message
};
