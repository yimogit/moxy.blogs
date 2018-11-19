<template>
  <div>
    <div class="blank"></div>
    <div class="blogs">
      <div class="bloglist">
        <h2><a href="#" title="程序员请放下你的技术情节，与你的同伴一起进步">程序员请放下你的技术情节，与你的同伴一起进步</a></h2>
        <p>如果说掌握一门赖以生计的技术是技术人员要学会的第一课的话， 那么我觉得技术人员要真正学会的第二课，不是技术，而是业务、交流与协作</p>
      </div>
      <div class="bloglist">
        <h2><a href="#" title="个人博客，我为什么要用帝国cms？">个人博客，我为什么要用帝国cms？</a></h2>
        <p>选择帝国cms，做一个博客系统，很多人都说是大材小用。确实是有那种感觉，因为cms有很多功能表用不上。但是帝国cms的安装包也就才6M，如果你放一张高清的1920的图，都比这安装包大！我就来说一说我为什么要用帝国cms。</p>
      </div>
      <div class="bloglist">
        <h2><a href="#" title="个人博客，属于我的小世界！">个人博客，属于我的小世界！</a></h2>
        <p>个人博客，用来做什么？我刚开始就把它当做一个我吐槽心情的地方，也就相当于一个网络记事本，写上一些关于自己生活工作中的小情小事，也会放上一些照片，音乐。每天工作回家后就能访问自己的网站，一边听着音乐，一边写写文章。</p>
      </div>
      <div class="bloglist">
        <h2><a href="#" title="程序员请放下你的技术情节，与你的同伴一起进步">程序员请放下你的技术情节，与你的同伴一起进步</a></h2>
        <p>如果说掌握一门赖以生计的技术是技术人员要学会的第一课的话， 那么我觉得技术人员要真正学会的第二课，不是技术，而是业务、交流与协作</p>
      </div>
      <div class="bloglist">
        <h2><a href="#" title="个人博客，我为什么要用帝国cms？">个人博客，我为什么要用帝国cms？</a></h2>
        <p>选择帝国cms，做一个博客系统，很多人都说是大材小用。确实是有那种感觉，因为cms有很多功能表用不上。但是帝国cms的安装包也就才6M，如果你放一张高清的1920的图，都比这安装包大！我就来说一说我为什么要用帝国cms。</p>
      </div>
      <div class="bloglist">
        <h2><a href="#" title="个人博客，属于我的小世界！">个人博客，属于我的小世界！</a></h2>
        <p>个人博客，用来做什么？我刚开始就把它当做一个我吐槽心情的地方，也就相当于一个网络记事本，写上一些关于自己生活工作中的小情小事，也会放上一些照片，音乐。每天工作回家后就能访问自己的网站，一边听着音乐，一边写写文章。</p>
      </div>
    </div>
    <aside>
      <div class="ztbox">
        <ul>
          <li> <a href="#" title="个人博客模板 ( 38 )">个人博客模板 ( 38 )</a></li>
          <li> <a href="#" title="国外html5模板 ( 12 )">国外html5模板 ( 12 )</a></li>
          <li><a href="#" title="心得笔记 ( 51 )">心得笔记 ( 51 )</a></li>
          <li><a href="#" title="CSS3|Html5 ( 63 )">CSS3|Html5 ( 63 )</a></li>
          <li><a href="#" title="JS经典实例 ( 10 )">JS经典实例 ( 10 )</a></li>
        </ul>
      </div>
      <div class="paihang">
        <h2>友情链接</h2>
        <ul>
          <li><a href="http://www.yangqq.com">杨青个人博客</a></li>
          <li><a href="http://www.yangqq.com">个人博客</a></li>
          <li><a href="http://www.yangqq.com">个人博客</a></li>
        </ul>
      </div>
    </aside>
  </div>
</template>

<script>
export default {
  data() {
    return {
      indexList: [],
      categoryList: [],
      hotList: [],
      recommendList: [],
      friendList: []
    }
  },
  asyncData(context) {
    // console.log(context.app.$api)
    // called every time before loading the component
    return { name: 'World' }
  }
}
</script>
