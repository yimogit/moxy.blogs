/**
 * 动态加载JS
 * @param {string} url 脚本地址
 * @param {function} callback  回调函数
 */
export function dynamicLoadJs(url, callback) {
  var head = document.getElementsByTagName('head')[0]
  var script = document.createElement('script')
  script.type = 'text/javascript'
  script.src = url
  if (typeof callback === 'function') {
    script.onload = script.onreadystatechange = function() {
      if (
        !this.readyState ||
        this.readyState === 'loaded' ||
        this.readyState === 'complete'
      ) {
        callback()
        script.onload = script.onreadystatechange = null
      }
    }
  }
  head.appendChild(script)
}
