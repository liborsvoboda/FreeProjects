<?php if (!defined('IN_PHPBB')) exit; $__file_count = (isset($this->_tpldata['_file'])) ? sizeof($this->_tpldata['_file']) : 0;if ($__file_count) {for ($__file_i = 0; $__file_i < $__file_count; ++$__file_i){$__file_val = &$this->_tpldata['_file'][$__file_i]; if ($__file_val['S_DENIED']) {  ?>

	<p>[<?php echo $__file_val['DENIED_MESSAGE']; ?>]</p>
	<?php } else { if ($__file_val['S_THUMBNAIL']) {  ?>

		<dl class="thumbnail">
			<dt><a href="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>"><img src="<?php echo $__file_val['THUMB_IMAGE']; ?>" alt="<?php echo $__file_val['DOWNLOAD_NAME']; ?>" title="<?php echo $__file_val['DOWNLOAD_NAME']; ?> (<?php echo $__file_val['FILESIZE']; ?> <?php echo $__file_val['SIZE_LANG']; ?>) <?php echo $__file_val['L_DOWNLOAD_COUNT']; ?>" /></a></dt>
			<?php if ($__file_val['COMMENT']) {  ?><dd> <?php echo $__file_val['COMMENT']; ?></dd><?php } ?>

		</dl>
		<?php } if ($__file_val['S_IMAGE']) {  ?>

		<dl class="file">
			<dt class="attach-image"><img src="<?php echo $__file_val['U_INLINE_LINK']; ?>" alt="<?php echo $__file_val['DOWNLOAD_NAME']; ?>" onclick="viewableArea(this);" /></dt>
			<?php if ($__file_val['COMMENT']) {  ?><dd><em><?php echo $__file_val['COMMENT']; ?></em></dd><?php } ?>

			<dd><?php echo $__file_val['DOWNLOAD_NAME']; ?> (<?php echo $__file_val['FILESIZE']; ?> <?php echo $__file_val['SIZE_LANG']; ?>) <?php echo $__file_val['L_DOWNLOAD_COUNT']; ?></dd>
		</dl>
		<?php } if ($__file_val['S_FILE']) {  ?>

		<dl class="file">
			<dt><?php if ($__file_val['UPLOAD_ICON']) {  echo $__file_val['UPLOAD_ICON']; ?> <?php } ?><a class="postlink" href="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>"><?php echo $__file_val['DOWNLOAD_NAME']; ?></a></dt>
			<?php if ($__file_val['COMMENT']) {  ?><dd><em><?php echo $__file_val['COMMENT']; ?></em></dd><?php } ?>

			<dd>(<?php echo $__file_val['FILESIZE']; ?> <?php echo $__file_val['SIZE_LANG']; ?>) <?php echo $__file_val['L_DOWNLOAD_COUNT']; ?></dd>
		</dl>
		<?php } if ($__file_val['S_WM_FILE']) {  ?><!-- method used here from http://alistapart.com/articles/byebyeembed / autosizing seems to not work always, this will not fix -->
			<object width="320" height="285" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" id="wmstream_<?php echo $__file_val['ATTACH_ID']; ?>">
				<param name="url" value="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>" />
				<param name="showcontrols" value="1" />
				<param name="showdisplay" value="0" />
				<param name="showstatusbar" value="0" />
				<param name="autosize" value="1" />
				<param name="autostart" value="0" />
				<param name="visible" value="1" />
				<param name="animationstart" value="0" />
				<param name="loop" value="0" />
				<param name="src" value="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>" />
				<!--[if !IE]>-->
					<object width="320" height="285" type="video/x-ms-wmv" data="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>">
						<param name="src" value="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>" />
						<param name="controller" value="1" />
						<param name="showcontrols" value="1" />
						<param name="showdisplay" value="0" />
						<param name="showstatusbar" value="0" />
						<param name="autosize" value="1" />
						<param name="autostart" value="0" />
						<param name="visible" value="1" />
						<param name="animationstart" value="0" />
						<param name="loop" value="0" />
					</object>
				<!--<![endif]-->
			</object>

		<?php } else if ($__file_val['S_FLASH_FILE']) {  ?>

			<object classid="clsid:D27CDB6E-AE6D-11CF-96B8-444553540000" codebase="http://active.macromedia.com/flash2/cabs/swflash.cab#version=5,0,0,0" width="<?php echo $__file_val['WIDTH']; ?>" height="<?php echo $__file_val['HEIGHT']; ?>">
				<param name="movie" value="<?php echo $__file_val['U_VIEW_LINK']; ?>" />
				<param name="play" value="true" />
				<param name="loop" value="true" />
				<param name="quality" value="high" />
				<param name="allowScriptAccess" value="never" />
				<param name="allowNetworking" value="internal" />
				<embed src="<?php echo $__file_val['U_VIEW_LINK']; ?>" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash" width="<?php echo $__file_val['WIDTH']; ?>" height="<?php echo $__file_val['HEIGHT']; ?>" play="true" loop="true" quality="high" allowscriptaccess="never" allownetworking="internal"></embed>
			</object>
		<?php } else if ($__file_val['S_QUICKTIME_FILE']) {  ?>

			<object id="qtstream_<?php echo $__file_val['ATTACH_ID']; ?>" classid="clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B" codebase="http://www.apple.com/qtactivex/qtplugin.cab#version=6,0,2,0" width="320" height="285">
				<param name="src" value="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>" />
				<param name="controller" value="true" />
				<param name="autoplay" value="false" />
				<param name="type" value="video/quicktime" />
				<embed name="qtstream_<?php echo $__file_val['ATTACH_ID']; ?>" src="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>" pluginspage="http://www.apple.com/quicktime/download/" enablejavascript="true" controller="true" width="320" height="285" type="video/quicktime" autoplay="false"></embed>
			</object>
		<?php } else if ($__file_val['S_RM_FILE']) {  ?>

			<object id="rmstream_<?php echo $__file_val['ATTACH_ID']; ?>" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" width="200" height="50">
				<param name="src" value="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>" />
				<param name="autostart" value="false" />
				<param name="controls" value="ImageWindow" />
				<param name="console" value="ctrls_<?php echo $__file_val['ATTACH_ID']; ?>" />
				<param name="prefetch" value="false" />
				<embed name="rmstream_<?php echo $__file_val['ATTACH_ID']; ?>" type="audio/x-pn-realaudio-plugin" src="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>" width="0" height="0" autostart="false" controls="ImageWindow" console="ctrls_<?php echo $__file_val['ATTACH_ID']; ?>" prefetch="false"></embed>
			</object>
			<br />
			<object id="ctrls_<?php echo $__file_val['ATTACH_ID']; ?>" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" width="0" height="36">
				<param name="controls" value="ControlPanel" />
				<param name="console" value="ctrls_<?php echo $__file_val['ATTACH_ID']; ?>" />
				<embed name="ctrls_<?php echo $__file_val['ATTACH_ID']; ?>" type="audio/x-pn-realaudio-plugin" width="200" height="36" controls="ControlPanel" console="ctrls_<?php echo $__file_val['ATTACH_ID']; ?>"></embed>
			</object>

			<script type="text/javascript">
			// <![CDATA[
				if (document.rmstream_<?php echo $__file_val['ATTACH_ID']; ?>.GetClipWidth)
				{
					while (!document.rmstream_<?php echo $__file_val['ATTACH_ID']; ?>.GetClipWidth())
					{
					}

					var width = document.rmstream_<?php echo $__file_val['ATTACH_ID']; ?>.GetClipWidth();
					var height = document.rmstream_<?php echo $__file_val['ATTACH_ID']; ?>.GetClipHeight();

					document.rmstream_<?php echo $__file_val['ATTACH_ID']; ?>.width = width;
					document.rmstream_<?php echo $__file_val['ATTACH_ID']; ?>.height = height;
					document.ctrls_<?php echo $__file_val['ATTACH_ID']; ?>.width = width;
				}
			// ]]>
			</script>
		<?php } if ($__file_val['S_WM_FILE'] || $__file_val['S_RM_FILE'] || $__file_val['S_FLASH_FILE'] || $__file_val['S_QUICKTIME_FILE']) {  ?>

			<p>
			<?php if ($__file_val['S_QUICKTIME_FILE']) {  ?><a href="#" onclick="play_qt_file(document.qtstream_<?php echo $__file_val['ATTACH_ID']; ?>); return false;">[ <?php echo ((isset($this->_rootref['L_PLAY_QUICKTIME_FILE'])) ? $this->_rootref['L_PLAY_QUICKTIME_FILE'] : ((isset($user->lang['PLAY_QUICKTIME_FILE'])) ? $user->lang['PLAY_QUICKTIME_FILE'] : '{ PLAY_QUICKTIME_FILE }')); ?> ]</a> <?php } ?>

			<a href="<?php echo $__file_val['U_DOWNLOAD_LINK']; ?>"><?php echo $__file_val['DOWNLOAD_NAME']; ?></a> [ <?php echo $__file_val['FILESIZE']; ?> <?php echo $__file_val['SIZE_LANG']; ?> | <?php echo $__file_val['L_DOWNLOAD_COUNT']; ?> ]</p>
		<?php } } }} ?>