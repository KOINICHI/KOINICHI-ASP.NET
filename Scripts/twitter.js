!function ($) {
    var Twitter = function () { }

    Twitter.prototype.updateTweet = function () {
        $.getJSON("api/tweetsapi", function (data) {
            $('#twitter > .tweet-box').html(
                data.Tweet + 
                '<div class="datetime">' + data.Date.replace('T', ' ') + '</div>');
        });
    }

    document.twitter = new Twitter();

    $(document).on('ready', function (e) {
        window.setInterval(this.twitter.updateTweet, 5000);
    });
}(jQuery)