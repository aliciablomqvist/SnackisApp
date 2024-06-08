$(document).ready(function () {
  console.log("Document is ready");
  $(".reply-button").click(function (event) {
    event.preventDefault();
    var commentId = $(this).data("comment-id");
    console.log("Reply button clicked for comment ID:", commentId);
    $("#ParentCommentId").val(commentId);
    $("html, body").animate(
      {
        scrollTop: $(".add-comment-form").offset().top,
      },
      1000
    );
  });
});
