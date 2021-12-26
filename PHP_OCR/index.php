<?php
require 'vendor/autoload.php';
require_once './vendor/thiagoalessio/tesseract_ocr/src/TesseractOCR.php';
use thiagoalessio\TesseractOCR\TesseractOCR;

/*$ocr = new TesseractOCR();
$ocr->image('/path/to/image.png');
$ocr->run();*/

echo (new TesseractOCR('text.png'))
    ->run();
?>