function padTo2Digits(num) {
    return num.toString().padStart(2, '0');
}

function getFormatDate(date) {
    return [
        padTo2Digits(date.getDate()),
        padTo2Digits(date.getMonth() + 1),
        date.getFullyear(),
    ].join('/');
}


function getFormatDate(date) {
    return [
        date.getFullYear(),
        padTo2Digits(date.getMonth() + 1),
        padTo2Digits(date.getDate()),

    ].join('-');
}

function getDateTime(pvalor) {
    const re = /-?\d+/;
    const m = re.exec(pvalor);
    return getFormatDate(new Date(parseInt(m[0], 10)));
}

function setDateTime(pValor) {
    const re = /-?\d+/;
    const m = re.exec(pValor);
    return setFormatDate(new Date(parseInt(m[0], 10)));
}
