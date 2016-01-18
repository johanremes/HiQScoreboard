$(document).ready(function () {
    initValues();
});

function initValues()
{
    onQuestionValueChangeEvent($('#LabelQ1'), 50);
    onQuestionValueChangeEvent($('#LabelQ2'), 50);
    onQuestionValueChangeEvent($('#LabelQ3'), 50);
    onQuestionValueChangeEvent($('#LabelQ4'), 50);
}


function between(val, min, max)
{
  return (val >= min && val <= max)
}

function onQuestionValueChangeEvent(element, value) {

    var txtVal = '';

    $(element).removeClass('bg-danger');
    $(element).removeClass('bg-primary');
    $(element).removeClass('bg-success');

    if (value < 69) {
        //$(element).addClass
        txtVal = '😞';
        $(element).addClass('bg-danger');
    }
    else if (value <= 89) {
        txtVal = '😐';
        $(element).addClass('bg-primary');
    }
    else if (value >= 90) {
        txtVal = '😄';
        $(element).addClass('bg-success');
    }
    txtVal += ' (' + value + ')';

    $(element).text(txtVal);
    

}