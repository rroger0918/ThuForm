

  (function () {
    // emailjs.init("USERID");
    emailjs.init("user_Ec3EGOLiAx79C0pwx9cEw");
  })();

  /**
    emailjs.send("SERVICE ID", "TEMPLATE NAME", {
      to_name: "USERNAME",
      from_name: "FROM NAME",
      message: "MESSAGE",
    });
   **/

  function validate() {
    let loader = document.querySelector(".loader");
    let name = document.querySelector(".user_name");
    let email = document.querySelector(".email");   
    let msg = document.querySelector(".message");
    let btn = document.querySelector(".submit");
    

    btn.addEventListener("click", (e) => {
      e.preventDefault();
      if (name.value == "" || email.value == "" || msg.value == "") {
        emptyerror();
      } else {
        loader.style.display = "flex";
        sendmail(name.value, email.value, msg.value);
        success();
        loader.style.display = "none";
      }
    });
  }
  validate();

  function sendmail(name, email, msg) {
    emailjs.send("service_38yxfmh", "template_ot6lgqg", {
      to_name: "GT, li",
      from_name: email,
      message: msg,
    });
  }

  function emptyerror() {
    Swal.fire({
      icon: "Error ! ",
      title: "Oops...",
      text: "The message cannot be blank ~",
    });
  }

  function error() {
    Swal.fire({
      icon: "Error !",
      title: "Oops...",
      text: "Something is wrong....Please send me a letter : leekuantean@gmail.com ,THX!",
    });
  }

  function success() {
    Swal.fire({
      icon: "success",
      title: "Successfully sent <3",
      text: "I will reply you as soon as possible ~",
    });
  }
