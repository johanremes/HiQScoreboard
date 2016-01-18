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

function onQuestionValueChangeEvent(element, value) {
    var txtVal = '';
    if (value < 39) 
        txtVal = '😞';
    else if (value <= 59) 
        txtVal = '😐';
    else if (value <= 89) 
        txtVal = '😊';
    else if (value >= 90) 
        txtVal = '😄';
    txtVal += ' ' + value;
    $(element).text(txtVal);
}