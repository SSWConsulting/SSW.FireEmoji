export function copyText(text) {
    navigator.clipboard.writeText(text).then(function () {
        return;
    })
    .catch(function (error) {
        console.error(error);
    });
}