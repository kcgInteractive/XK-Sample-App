System.register(["react","@kentico/xperience-admin-components"],(function(e,t){var n={},r={};return{setters:[function(e){n.Suspense=e.Suspense,n.default=e.default},function(e){r.MenuItem=e.MenuItem,r.Select=e.Select}],execute:function(){e((()=>{var e={722:(e,t,n)=>{const r=n(905).R;t.s=function(e){if(e||(e=1),!n.y.meta||!n.y.meta.url)throw console.error("__system_context__",n.y),Error("systemjs-webpack-interop was provided an unknown SystemJS context. Expected context.meta.url, but none was provided");n.p=r(n.y.meta.url,e)}},905:(e,t,n)=>{t.R=function(e,t){var n=document.createElement("a");n.href=e;for(var r="/"===n.pathname[0]?n.pathname:"/"+n.pathname,o=0,a=r.length;o!==t&&a>=0;)"/"===r[--a]&&o++;if(o!==t)throw Error("systemjs-webpack-interop: rootDirectoryLevel ("+t+") is greater than the number of directories ("+o+") in the URL path "+e);var l=r.slice(0,a+1);return n.protocol+"//"+n.host+l};Number.isInteger},271:e=>{"use strict";e.exports=r},954:e=>{"use strict";e.exports=n}},o={};function a(t){var n=o[t];if(void 0!==n)return n.exports;var r=o[t]={exports:{}};return e[t](r,r.exports,a),r.exports}a.y=t,a.d=(e,t)=>{for(var n in t)a.o(t,n)&&!a.o(e,n)&&Object.defineProperty(e,n,{enumerable:!0,get:t[n]})},a.o=(e,t)=>Object.prototype.hasOwnProperty.call(e,t),a.r=e=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},a.p="";var l={};return(0,a(722).s)(1),(()=>{"use strict";a.r(l),a.d(l,{AssetSelectorFormComponent:()=>o,WebsiteChannelFormComponent:()=>n});var e=a(954),t=a(271);const n=n=>e.default.createElement(t.Select,{onChange:e=>n.onChange?.(e),label:"Select Channel",invalid:n.invalid,validationMessage:n.validationMessage,markAsRequired:n.required},n.options?.map(((n,r)=>e.default.createElement(t.MenuItem,{primaryLabel:n.text,value:n.value,key:r})))),r=()=>(fetch("http://localhost:5000/api/select").then((e=>e.json())).then((e=>console.log(e))),e.default.createElement("div",null)),o=t=>e.default.createElement("div",null,e.default.createElement(e.Suspense,{fallback:e.default.createElement("div",null,"Loading...")},e.default.createElement(r,null)))})(),l})())}}}));