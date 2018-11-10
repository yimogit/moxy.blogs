export default {
  auth: {
    inserted: (el, binding) => {
      if (binding.value && !checkAuth(binding.value)) {
        if (binding.arg === 'remove') {
          el.remove()
        } else {
          el.className += ' is-disabled '
          el.setAttribute('disabled', 'disabled')
        }
      }
    }
  }
}

function checkAuth(code) {
  return (
    window.authInfo &&
    window.authInfo.modules &&
    (window.authInfo.modules.indexOf(code) > -1 ||
      window.authInfo.modules[0] === '*')
  )
}
