video = document.querySelector("#videoElement");
if (navigator.mediaDevices) {
    navigator.mediaDevices.getUserMedia({ audio: false, video: true })
        .then(function (mediaStream) {
            video.srcObject = mediaStream;
            video.onloadedmetadata = function (e) {
                video.play();
            };
        })
        .catch(function (err) { console.log(err.name + ": " + err.message); }); // always check for errors at the end.

    QCodeDecoder().decodeFromVideo(video, function (er, res) {
        console.log(res);
        document.getElementById("qrContent").innerHTML = res;
        if (res != undefined) {
            window.location.href = '/Plant/Details/' + res;
        }
    });
}


