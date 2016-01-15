/*$(document).ready(function () {

    $('#btnExport').click(function () {

        window.location = '/Scoreboard/ExportToExcel';
    }
   );
});
*/
function onQuestionValueChangeEvent(element, value) {
    $(element).text(value + '%');
}