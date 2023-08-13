const user = localStorage.getItem("user")
if(user) {
  window.location.replace('./index.html')
}

const domain = 'https://localhost:7288/'
const signIn = document.getElementById('signIn')
console.log(signIn);
signIn.addEventListener('click', (e) => {
  e.preventDefault()
  let username = document.getElementById('username').value
  let password = document.getElementById('password').value
  let user = {
    username, password
  }

  localStorage.setItem('user', JSON.stringify(user))
  window.location.reload()
})