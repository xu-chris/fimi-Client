(function(e){function t(t){for(var r,o,l=t[0],u=t[1],c=t[2],f=0,s=[];f<l.length;f++)o=l[f],Object.prototype.hasOwnProperty.call(a,o)&&a[o]&&s.push(a[o][0]),a[o]=0;for(r in u)Object.prototype.hasOwnProperty.call(u,r)&&(e[r]=u[r]);d&&d(t);while(s.length)s.shift()();return i.push.apply(i,c||[]),n()}function n(){for(var e,t=0;t<i.length;t++){for(var n=i[t],r=!0,o=1;o<n.length;o++){var l=n[o];0!==a[l]&&(r=!1)}r&&(i.splice(t--,1),e=u(u.s=n[0]))}return e}var r={},o={1:0},a={1:0},i=[];function l(e){return u.p+"js/"+({}[e]||e)+"."+{2:"2736154d",3:"bbdf58fd",4:"2c96cba1",5:"ef431229",6:"856f76d3",7:"e7ebbecc",8:"70970c45"}[e]+".js"}function u(t){if(r[t])return r[t].exports;var n=r[t]={i:t,l:!1,exports:{}};return e[t].call(n.exports,n,n.exports,u),n.l=!0,n.exports}u.e=function(e){var t=[],n={2:1,3:1};o[e]?t.push(o[e]):0!==o[e]&&n[e]&&t.push(o[e]=new Promise((function(t,n){for(var r="css/"+({}[e]||e)+"."+{2:"51144fb4",3:"51144fb4",4:"31d6cfe0",5:"31d6cfe0",6:"31d6cfe0",7:"31d6cfe0",8:"31d6cfe0"}[e]+".css",a=u.p+r,i=document.getElementsByTagName("link"),l=0;l<i.length;l++){var c=i[l],f=c.getAttribute("data-href")||c.getAttribute("href");if("stylesheet"===c.rel&&(f===r||f===a))return t()}var s=document.getElementsByTagName("style");for(l=0;l<s.length;l++){c=s[l],f=c.getAttribute("data-href");if(f===r||f===a)return t()}var d=document.createElement("link");d.rel="stylesheet",d.type="text/css",d.onload=t,d.onerror=function(t){var r=t&&t.target&&t.target.src||a,i=new Error("Loading CSS chunk "+e+" failed.\n("+r+")");i.code="CSS_CHUNK_LOAD_FAILED",i.request=r,delete o[e],d.parentNode.removeChild(d),n(i)},d.href=a;var p=document.getElementsByTagName("head")[0];p.appendChild(d)})).then((function(){o[e]=0})));var r=a[e];if(0!==r)if(r)t.push(r[2]);else{var i=new Promise((function(t,n){r=a[e]=[t,n]}));t.push(r[2]=i);var c,f=document.createElement("script");f.charset="utf-8",f.timeout=120,u.nc&&f.setAttribute("nonce",u.nc),f.src=l(e);var s=new Error;c=function(t){f.onerror=f.onload=null,clearTimeout(d);var n=a[e];if(0!==n){if(n){var r=t&&("load"===t.type?"missing":t.type),o=t&&t.target&&t.target.src;s.message="Loading chunk "+e+" failed.\n("+r+": "+o+")",s.name="ChunkLoadError",s.type=r,s.request=o,n[1](s)}a[e]=void 0}};var d=setTimeout((function(){c({type:"timeout",target:f})}),12e4);f.onerror=f.onload=c,document.head.appendChild(f)}return Promise.all(t)},u.m=e,u.c=r,u.d=function(e,t,n){u.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:n})},u.r=function(e){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},u.t=function(e,t){if(1&t&&(e=u(e)),8&t)return e;if(4&t&&"object"===typeof e&&e&&e.__esModule)return e;var n=Object.create(null);if(u.r(n),Object.defineProperty(n,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var r in e)u.d(n,r,function(t){return e[t]}.bind(null,r));return n},u.n=function(e){var t=e&&e.__esModule?function(){return e["default"]}:function(){return e};return u.d(t,"a",t),t},u.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},u.p="",u.oe=function(e){throw console.error(e),e};var c=window["webpackJsonp"]=window["webpackJsonp"]||[],f=c.push.bind(c);c.push=t,c=c.slice();for(var s=0;s<c.length;s++)t(c[s]);var d=f;i.push([0,0]),n()})({0:function(e,t,n){e.exports=n("2f39")},"0047":function(e,t,n){},"2f39":function(e,t,n){"use strict";n.r(t);n("e6cf"),n("5319"),n("7d6e"),n("e54f"),n("985d"),n("0047");var r=n("2b0e"),o=n("1f91"),a=n("42d2"),i=n("b05d");r["a"].use(i["a"],{config:{},lang:o["a"],iconSet:a["a"]});var l=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("div",{attrs:{id:"q-app"}},[n("router-view")],1)},u=[],c={name:"App"},f=c,s=n("2877"),d=Object(s["a"])(f,l,u,!1,null,null,null),p=d.exports,h=n("8c4f");const m=[{path:"/",component:()=>Promise.all([n.e(0),n.e(3)]).then(n.bind(null,"713b")),children:[{path:"",component:()=>Promise.all([n.e(0),n.e(7)]).then(n.bind(null,"8b24"))},{path:"/training/:id",name:"Training",component:()=>Promise.all([n.e(0),n.e(8)]).then(n.bind(null,"2f36"))}]},{path:"/training/:id/in-training",name:"In Training",component:()=>Promise.all([n.e(0),n.e(2)]).then(n.bind(null,"f363")),children:[{path:"",component:()=>Promise.all([n.e(0),n.e(6)]).then(n.bind(null,"af8f"))},{path:"/training/:id/after-training",name:"After Training",component:()=>Promise.all([n.e(0),n.e(5)]).then(n.bind(null,"85da"))}]},{path:"*",component:()=>Promise.all([n.e(0),n.e(4)]).then(n.bind(null,"ee5d"))}];var b=m;r["a"].use(h["a"]);var g=function(){const e=new h["a"]({scrollBehavior:()=>({x:0,y:0}),routes:b,mode:"hash",base:""});return e},v=async function(){const e="function"===typeof g?await g({Vue:r["a"]}):g,t={router:e,render:e=>e(p),el:"#q-app"};return{app:t,router:e}},y=n("bc3a"),w=n.n(y);r["a"].prototype.$axios=w.a;const P="";async function O(){const{app:e,router:t}=await v();let n=!1;const o=e=>{n=!0;const r=Object(e)===e?t.resolve(e).route.fullPath:e;window.location.href=r},a=window.location.href.replace(window.location.origin,""),i=[void 0];for(let u=0;!1===n&&u<i.length;u++)if("function"===typeof i[u])try{await i[u]({app:e,router:t,Vue:r["a"],ssrContext:null,redirect:o,urlPath:a,publicPath:P})}catch(l){return l&&l.url?void(window.location.href=l.url):void console.error("[Quasar] boot error:",l)}!0!==n&&new r["a"](e)}O()}});