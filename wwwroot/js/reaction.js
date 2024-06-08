$(document).ready(function () {
  $(".reaction-button").click(function (event) {
    event.preventDefault();

    var button = $(this);
    var postId = button.data("post-id");
    var reactionType = button.data("reaction");

    $.ajax({
      url: "/api/reactions",
      method: "POST",
      data: {
        postId: postId,
        reactionType: reactionType,
      },
      success: function (response) {
        if (response.success) {
          var countSpan = button.find("span");
          var currentCount = parseInt(countSpan.text());
          countSpan.text(currentCount + 1);
        } else {
          alert("Failed to add reaction");
        }
      },
      error: function () {
        alert("Error adding reaction");
      },
    });
  });
});
